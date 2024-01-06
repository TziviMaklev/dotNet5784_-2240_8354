using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    #region help methods
    /// <summary>
    /// Creates milestones based on the received dependencies.
    /// </summary>
    /// <param name="receivedDeps">The list of received dependencies.</param>
    /// <returns>The list of created milestones.</returns>
    internal List<DO.Dependency> creatingMilestones(List<DO.Dependency> recievedDeps)
    {
        #region declares of help IEnumerables
        List<DO.Task> allOldTasks = _dal.Task.ReadAll().ToList()!;
        // A grouped list so that the key is a dependent task and the value is a list of previous tasks
        var groupByTaskDependency = (from dep in recievedDeps
                                     group dep by dep.DependentTask into groupByDependentTask
                                     let depList = (from dep in groupByDependentTask
                                                    select dep.DependenceOnTask).Order()
                                     select new { _key = groupByDependentTask.Key, _value = depList });

        // Another filtered list of the list of values so that each value appears once
        var taskDependencyWhithoutDuplicate = (from dep in groupByTaskDependency
                                               select dep._value.ToList()).Distinct().ToList();
        #endregion
        List<DO.Dependency> newDependendiesList = new List<DO.Dependency>();
        // Go through all the elements in the list taskDependencyWhithoutDuplicate
        // and create a milestone for each value
        int i = 1;
        foreach (var groupOfDepentOnTasks in taskDependencyWhithoutDuplicate)
        {   //create milstone for each one
            DO.Task milestone = new DO.Task(-1, "milestone description", $"M{i}",
                true, null, null, TimeSpan.FromDays(0), DateTime.Now, null);
            int idMilestone = _dal.Task.Create(milestone);

            foreach (var taskListwithDeps in groupByTaskDependency)
            {   //set each task that depent on this milestone
                var t = taskListwithDeps._value.ToList();
                if (t.SequenceEqual(groupOfDepentOnTasks))
                    newDependendiesList.Add(new DO.Dependency(-1, taskListwithDeps._key!, idMilestone));
            }

            foreach (var dep in groupOfDepentOnTasks)
            {   //set each task that this milestone depent on it
                newDependendiesList.Add(new DO.Dependency(-1, idMilestone, dep));
            }
            i++;
        }

        #region make the start milestone
        int idStartMileston = _dal.Task.Create(new DO.Task
        {
            Id = 0,
            Alias = $"start",
            Description = "description",
            Milestone = true,
            Deliverables = "",
            ComplexityTask = DO.EngineerExperience.Expert,
            CreationDate = DateTime.Now,
            RequiredEffortTime = TimeSpan.FromDays(0)
        });
        //find the tasks that not dependnce on anything
        var independentTasks = (from task in allOldTasks
                                where !(from taskDep in groupByTaskDependency
                                        select taskDep._key).Any(t => t == task.Id)
                                select task.Id);
        foreach (var task in independentTasks)
        {
            newDependendiesList.Add(new Dependency(0, task, idStartMileston));
        }
        #endregion
        #region make the end milestone
        int idLastMileston = _dal.Task.Create(new DO.Task
        {
            Id = 0,
            Alias = $"end",
            Description = "description",
            Milestone = true,
            Deliverables = "",
            ComplexityTask = DO.EngineerExperience.Expert,
            CreationDate = DateTime.Now,
            RequiredEffortTime = TimeSpan.FromDays(0)
        });
        //find tasks that no task depends on them
        var endDepTasks = (from task in allOldTasks
                           where !(from dep in recievedDeps
                                   select dep.DependenceOnTask).Distinct().Any(t => t == task.Id)
                           select task.Id);
        foreach (var task in endDepTasks)//create dep for tasks that end depend on them
        {
            newDependendiesList.Add(new DO.Dependency(-1, idLastMileston, task));
        }
        #endregion
        return newDependendiesList;
    }

    /// <summary>
    /// Updates the deadlines for a task based on dependencies and milestones.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="endMilestoneId">The ID of the end milestone.</param>
    /// <param name="depList">The list of dependencies.</param>
    /// <returns>The updated deadline for the task.</returns>
    private DateTime? updateDeadlines(int taskId, int endMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == endMilestoneId)
            return _dal.EndProject;
        DO.Task currTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        //ids of tasks that depends on curr task
        List<int> DepentOnCurrentTask = (from dep in depList
                                         where dep.DependenceOnTask == taskId
                                         select dep.DependentTask).ToList();
        DateTime? deadline = currTask.DeadlineDate;
        //check for each task what about it's deadline and update that

        foreach (var dep in DepentOnCurrentTask)
        {
            // Read the task from the data access layer
            DO.Task readTsk = _dal.Task.Read(dep) ?? throw new BlNullPropertyException($"Task with Id {dep} does not exists");
            if (readTsk.DeadlineDate is null)//no deadline yet
                 // If the task has no deadline, update it with the calculated deadline
                readTsk = readTsk with { DeadlineDate = updateDeadlines(dep, endMilestoneId, depList) };
            else if (deadline is null
                || readTsk.DeadlineDate - readTsk.RequiredEffortTime < deadline)//not good deadline
                 // If the task has a deadline, check if it is earlier than the current deadline
                // If so, update the current deadline with the task's deadline
                deadline = readTsk.DeadlineDate - readTsk.RequiredEffortTime;
        }
        if (deadline > _dal.EndProject)
            throw new BlTimeSurfing("There is insufficient time to complete this task\n");
        currTask = currTask with { DeadlineDate = deadline };//update
        _dal.Task.Update(currTask);
        return currTask.DeadlineDate;
    }

    /// <summary>
    /// Updates the scheduled dates for a task based on dependencies and milestones.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="startMilestoneId">The ID of the start milestone.</param>
    /// <param name="depList">The list of dependencies.</param>
    /// <returns>The updated scheduled date for the task.</returns>
    private DateTime? updateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
    {
        // Check if the current task is the start milestone - stop the recursion
        if (taskId == startMilestoneId)
            return _dal.StartProject;

        // Read the current task from the data access layer
        DO.Task currTask = _dal.Task.Read(taskId)
            ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");

        // Get the list of tasks that the current task depends on
        var tasksCurrTaskDependndsOn = (from dep in depList
                                        where dep.DependentTask == taskId
                                        select dep.DependenceOnTask).ToList();
        DateTime? scheduledDate = currTask.ScheduledDate;

        foreach (int deps in tasksCurrTaskDependndsOn)
        {
            // Read the task that the current task depends on
            DO.Task readTask = _dal.Task.Read(deps) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");

            if (readTask.ScheduledDate is null)
                // If the task has no scheduled date, update it with the calculated scheduled date
                readTask = readTask with { ScheduledDate = updateScheduledDates(deps, startMilestoneId, depList) };

            if (scheduledDate is null || readTask.ScheduledDate + readTask.RequiredEffortTime > scheduledDate)
                // If the task has a scheduled date, check if the calculated scheduled date is later
                // If so, update the current scheduled date with the task's scheduled date
                scheduledDate = readTask.ScheduledDate + readTask.RequiredEffortTime;
        }


        if (scheduledDate < _dal.StartProject)
            // Throw an exception if the calculated scheduled date is earlier than the project start date
            throw new BO.BlTimeSurfing("There is insufficient time to complete this task\n");

        // Update the current task's scheduled date in the data access layer
        currTask = currTask with { ScheduledDate = scheduledDate };
        _dal.Task.Update(currTask);

        return currTask.ScheduledDate;
    }
    #endregion

    /// <summary>
    /// Creates the milestone project schedule.
    /// </summary>
    public void CreatingTheMilestoneProjectSchedule()
    {
        // Read all dependencies from the data access layer
        List<DO.Dependency?> depnedencs = _dal.Dependency.ReadAll().ToList();

        // Create a new list of dependencies by creating milestones
        List<DO.Dependency> newDependecyList = creatingMilestones(depnedencs);
        //Replace all dependencies in a milestone task's dependencies

        // Reset all dependencies in the data access layer
        _dal.Dependency.Reset();

        // Create each dependency in the new dependency list
        foreach (var dep in newDependecyList)
        {
            _dal.Dependency.Create(dep!);
        }

        // Read all tasks from the data access layer
        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();

        int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();//get the milestone of start project

        // Read the start milestone task from the data access layer
        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;

        // Set the start project date for the start milestone task
        if (startMilestone is not null)
            startMilestone = startMilestone with { ScheduledDate = _dal.StartProject };//set the start project date

        int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();//get the milestone of end project

        // Read the end milestone task from the data access layer
        DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;

        // Set the deadline project date for the end milestone task
        if (endMilestone is not null)
            endMilestone = endMilestone with { DeadlineDate = _dal.EndProject };//set the deadline project date

        // Update the deadline date for the start milestone task
        startMilestone = startMilestone! with { DeadlineDate = updateDeadlines(startMilestoneId, endMilestoneId, newDependecyList) };
        _dal.Task.Update(startMilestone!);

        // Update the scheduled date for the end milestone task
        endMilestone = endMilestone! with { ScheduledDate = updateScheduledDates(endMilestoneId, startMilestoneId, newDependecyList) };
        _dal.Task.Update(endMilestone);
    }

    /// <summary>
    /// Retrieves a milestone with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the milestone to retrieve.</param>
    /// <returns>The milestone object with the specified ID, or null if not found.</returns>
    public Milestone Read(int id)
    {
        DO.Task doMilestoneTask = _dal.Task.Read(id) ?? throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        DateTime? forcadeDate = null;
        #region find help vars
        //find the forcade date
        if (doMilestoneTask.StartDate is not null)
        {
            forcadeDate = doMilestoneTask.StartDate + doMilestoneTask.RequiredEffortTime;
        }
        //find the progress percentage
        float progressPercentage = (float)((DateTime.Now - doMilestoneTask.StartDate) / doMilestoneTask.RequiredEffortTime)! * 100;
        if (progressPercentage > 100)
            progressPercentage = 100;
        #endregion
        //make a new milestone from the task milestone
        BO.Milestone milestone = new()
        {
            Id = doMilestoneTask.Id,
            Alias = doMilestoneTask.Alias,
            Description = doMilestoneTask.Description,
            CreateDate = doMilestoneTask.CreationDate,
            Status = (Status)(doMilestoneTask.ScheduledDate is null ? 0 :
                               doMilestoneTask.StartDate is null ? 1 :
                               doMilestoneTask.CompletionDate is null ? 2
                               : 3),
            ForecastDate = forcadeDate,
            Deadline = doMilestoneTask.DeadlineDate,
            ActualEndDate = doMilestoneTask.CompletionDate,
            ProgressPercentage = progressPercentage,
            Remarks = doMilestoneTask.Remarks,
        };
        return milestone;
    }

    /// <summary>
    /// Updates the information or status of a specific milestone.
    /// </summary>
    /// <param name="milestone">The milestone object representing the milestone to be updated.</param>
    public void UpdateMilestone(Milestone milestone)
    {
        // Create a new DO.Task object using the milestone properties
        DO.Task doTask = new DO.Task(milestone.Id,milestone.Description
            ,milestone.Alias,true, "",null,new TimeSpan(0), (DateTime)milestone.CreateDate!,
            milestone.StartDate,null,milestone.Deadline,milestone.ActualEndDate,milestone.Remarks,null);

        try
        {
            // Update the task in the data access layer
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            // If the task does not exist in the data access layer, throw a custom exception
            throw new BlDoesNotExistException($"An object of type Task with ID {doTask.Id} does not exist", ex);
        }
    }

    /// <summary>
    /// set the start and the end date of the project
    /// </summary>
    /// <param name="start">The start date of the task or event.</param>
    /// <param name="end">The end date of the task or event.</param>
    /// <remarks>
    /// This method is used to set the start and end dates for the project.
    /// It takes two parameters, "start" and "end", of type DateTime to specify the start and end dates respectively.
    /// The method does not return any value.
    /// </remarks>
    public void SetDates(DateTime start, DateTime end)
    {
        _dal.StartProject = start;
        _dal.EndProject = end;
    }
}

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
            DO.Task readTsk = _dal.Task.Read(dep) ?? throw new BlNullPropertyException($"Task with Id {dep} does not exists");
            if (readTsk.DeadlineDate is null)//no deadline yet
                readTsk = readTsk with { DeadlineDate = updateDeadlines(dep, endMilestoneId, depList) };
            else if (deadline is null
                || readTsk.DeadlineDate - readTsk.RequiredEffortTime < deadline)//not good deadline
                deadline = readTsk.DeadlineDate - readTsk.RequiredEffortTime;
        }
        if (deadline > _dal.EndProject)
            throw new BlTimeSurfing("There is insufficient time to complete this task\n");
        currTask = currTask with { DeadlineDate = deadline };//update
        _dal.Task.Update(currTask);
        return currTask.DeadlineDate;
    }

    private DateTime? updateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == startMilestoneId)//the start milestone - stop the recorse
            return _dal.StartProject;
        DO.Task currTask = _dal.Task.Read(taskId)
            ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        var tasksCurrTaskDependndsOn = (from dep in depList
                                        where dep.DependentTask == taskId
                                        select dep.DependenceOnTask).ToList();
        DateTime? scheduledDate = currTask.ScheduledDate;
        foreach (int deps in tasksCurrTaskDependndsOn)
        {
            DO.Task readTask = _dal.Task.Read(deps) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
            if (readTask.ScheduledDate is null)
                readTask = readTask with { ScheduledDate = updateScheduledDates(deps, startMilestoneId, depList) };
            if (scheduledDate is null || readTask.ScheduledDate + readTask.RequiredEffortTime > scheduledDate)
                scheduledDate = readTask.ScheduledDate + readTask.RequiredEffortTime;
        }
        if (scheduledDate < _dal.StartProject)
            throw new BO.BlTimeSurfing("There is insufficient time to complete this task\n");
        currTask = currTask with { ScheduledDate = scheduledDate };
        _dal.Task.Update(currTask);
        return currTask.ScheduledDate;
    }
    #endregion

    public void CreatingTheMilestoneProjectSchedule()
    {
        List<DO.Dependency?> depnedencs = _dal.Dependency.ReadAll().ToList();
        List<DO.Dependency> newDependecyList = creatingMilestones(depnedencs);
        //Replace all dependencies in a milestone task's dependencies

        _dal.Dependency.Reset();
        foreach (var dep in newDependecyList)
        {
            _dal.Dependency.Create(dep!);
        }
        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();//get the milestone of start project

        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
        if (startMilestone is not null)
            startMilestone = startMilestone with { ScheduledDate = _dal.StartProject };//set the start project date

        int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();//get the milestone of end project

        DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;
        if (endMilestone is not null)
            endMilestone = endMilestone with { DeadlineDate = _dal.EndProject };//set the deadline project date

        startMilestone = startMilestone! with { DeadlineDate = updateDeadlines(startMilestoneId, endMilestoneId, newDependecyList) };
        _dal.Task.Update(startMilestone!);

        endMilestone = endMilestone! with { ScheduledDate = updateScheduledDates(endMilestoneId, startMilestoneId, newDependecyList) };
        _dal.Task.Update(endMilestone);
    }

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

    public void UpdateMilestone(Milestone milestone)
    {
        DO.Task doTask = new DO.Task(milestone.Id, milestone.Description
            , milestone.Alias, true, "", null, new TimeSpan(0), (DateTime)milestone.CreateDate!,
            milestone.StartDate, null, milestone.Deadline, milestone.ActualEndDate, milestone.Remarks, null);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"An object of type Task with ID {doTask.Id} does not exist", ex);
        }
    }

    public void SetDates(DateTime start, DateTime end)
    {
        _dal.StartProject = start;
        _dal.EndProject = end;
    }
}

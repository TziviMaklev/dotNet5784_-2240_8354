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
        // A grouped list so that the key is a dependent task and the value is a list of previous tasks
        var groupByTaskDependency = (from dependency in recievedDeps
                                     where dependency?.DependentTask != null && dependency?.DependenceOnTask != null
                                     group dependency by dependency.DependentTask
                                     into dependencyListAfterGrouping
                                     let dependencyList = (from dependency in dependencyListAfterGrouping
                                                           select dependency.DependenceOnTask).Order()
                                     select new { _key = dependencyListAfterGrouping.Key, _value = dependencyList }).ToList();

        // Another filtered list of the list of values so that each value appears once
        var taskDependencyWhithoutDuplicate = (from dependency in groupByTaskDependency
                                               select dependency._value).Distinct();
        #endregion
        List<DO.Dependency> newDependendiesList = new List<DO.Dependency>();
        // Go through all the elements in the list taskDependencyWhithoutDuplicate
        // and create a milestone for each value
        foreach (var dep in taskDependencyWhithoutDuplicate)
        {
            DO.Task milestone = new DO.Task()
            {
                Id = 0,
                Alias = $"M{BO.Tools.Config.NextMilestoneId}",
                Description = "description",
                Milestone = true,
                Deliverables = "",
                ComplexityTask = DO.EngineerExperience.Expert,
                CreationDate = DateTime.Now,
                RequiredEffortTime = TimeSpan.FromDays(0)
            };
            //create a new milestone
            int idNewMilestone = _dal.Task.Create(milestone);
            //making deps for the all tasks that right after the milestones
            foreach (var milestonDep in groupByTaskDependency)
            {
                if (milestonDep._value == dep)
                    recievedDeps.Add(new Dependency(0, milestonDep._key, idNewMilestone));
            }
            //add the dependencies of the milestone to the new list
            foreach (var p in dep)
            {
                recievedDeps.Add(new Dependency(0, idNewMilestone, p));
            }
        }

        #region make the start milestone
        int idFirstMileston = _dal.Task.Create(new DO.Task
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
        var independentTasks = (from t in _dal.Task.ReadAll().ToList()
                                join d in _dal.Dependency.ReadAll() on t.Id equals d.DependentTask into dependentTasks
                                where dependentTasks.Count() == 0
                                select t).ToList();
        foreach (var task in independentTasks)
        {
            recievedDeps.Add(new Dependency(0, task.Id, idFirstMileston));
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
        var endDepTasks = (from task in _dal.Task.ReadAll().ToList()
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
        DateTime? deadline = null;
        //check for each task what about it's deadline and update that

        foreach (var dep in DepentOnCurrentTask)
        {
            DO.Task readTsk = _dal.Task.Read(dep)!;
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
        if (taskId == startMilestoneId)
            return _dal.StartProject;
        DO.Task currTask = _dal.Task.Read(taskId)
            ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        var tasksCurrTaskDependndsOn = (from dep in depList
                                        where dep.DependentTask == taskId
                                        select dep.DependenceOnTask).ToList();
        DateTime? scheduledDate = null;
        foreach (int deps in tasksCurrTaskDependndsOn)
        {
            DO.Task readTask = _dal.Task.Read(deps) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
            if (readTask.ScheduledDate is null)
                readTask = readTask with { ScheduledDate = updateScheduledDates(taskId, startMilestoneId, depList) };
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
        List<DO.Dependency> newDependecyList = creatingMilestones(depnedencs!);
        //Replace all dependencies in a milestone task's dependencies
        _dal.Dependency.Reset();
        foreach (var dep in newDependecyList)
        {
            _dal.Dependency.Create(dep);
        }
        List<DO.Task> allTasks = _dal.Task.ReadAll().ToList()!;
        //find the start milestone and update it's schedual date
        int startMilestoneId = allTasks.Where(t => t.Alias == "start").Select(t => t.Id).FirstOrDefault();
        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
        if (startMilestone is not null)
            startMilestone = startMilestone with { ScheduledDate = _dal.StartProject };
        //find the end milestone and update it's schedual date
        int endMilestonId = allTasks.Where(t => t.Alias == "end").Select(t => t.Id).FirstOrDefault();
        DO.Task endMilestone = _dal.Task.Read(endMilestonId)!;
        if (endMilestone is not null)
            endMilestone = endMilestone with { DeadlineDate = _dal.EndProject };

        //update all deadlines and schedual dates
        startMilestone = startMilestone! with
        { DeadlineDate = updateDeadlines(startMilestoneId, endMilestonId, newDependecyList) };
        _dal.Task.Update(startMilestone);
        endMilestone = endMilestone! with
        { ScheduledDate = updateScheduledDates(endMilestonId, startMilestoneId, newDependecyList) };
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
            ProgressPercentage=progressPercentage,
            Remarks=doMilestoneTask.Remarks,
        };
        return milestone;
    }

    public void UpdateMilestone(Milestone milestone)
    {
        DO.Task doTask = new DO.Task(milestone.Id,milestone.Description
            ,milestone.Alias,true, "",null,new TimeSpan(0), (DateTime)milestone.CreateDate!,
            milestone.StartDate,null,milestone.Deadline,milestone.ActualEndDate,milestone.Remarks,null);
        try
        {
            _dal.Task.Update(doTask);
        }catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"An object of type Task with ID {doTask.Id} does not exist", ex);
        }
    }
}

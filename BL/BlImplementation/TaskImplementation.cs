
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using ITask = BlApi.ITask;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEnumerable<BO.TaskInList>? RequestTaskList(Func<BO.TaskInList, bool>? filter = null)
    {
        var taskList = (from DO.Task doTask in _dal.Task.ReadAll()
                        select new BO.TaskInList
                        {
                            Id = doTask.Id,
                            Alias = doTask.Alias,
                            Description = doTask.Description,
                            Status = (BO.Status)(doTask.ScheduledDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.CompletionDate is null ? 2
                                               : 3)
                        });
        if (filter != null)
        {
            var taskListWhithFilter = (from item in taskList
                                       where filter(item)
                                       select item);

            return taskListWhithFilter;
        }
        else
        {
            return taskList;
        }
    }
    public BO.Task GetTask(int id)
    {
        try
        {
            DO.Task? task = _dal.Task.Read(id);
            return new BO.Task()
            {
                Id = task!.Id,
                Alias = task!.Alias,
                Description = task!.Description,
                CreatedAtDate = task!.CreationDate,
                Status = (BO.Status)(task.ScheduledDate is null ? 0
                                               : task.StartDate is null ? 1
                                               : task.CompletionDate is null ? 2
                                               : 3),
                Dependencies = (from DO.Dependency doDependency in _dal.Dependency!.ReadAll(d => d.DependentTask == task!.Id)
                                where true
                                select new BO.TaskInList()
                                {
                                    Id = doDependency.DependenceOnTask,
                                    Alias = _dal.Task!.Read(doDependency.DependenceOnTask)?.Alias,
                                    Description = _dal.Task!.Read(doDependency.DependenceOnTask)?.Description,
                                    Status = (BO.Status)(
                                    _dal.Task!.Read(doDependency.DependenceOnTask)?.ScheduledDate is null ? 0
                                    : _dal.Task!.Read(doDependency.DependenceOnTask)?.StartDate is null ? 1
                                    : _dal.Task!.Read(doDependency.DependenceOnTask)?.CompletionDate is null ? 2
                                    : 3)
                                }).ToList(),
                Milstone = new BO.MilstoneInTask()
                {
                    Id = _dal.Task!.Read(_dal.Dependency!.Read(dep =>
                    {
                        _dal.Task!.Read(task => task.Milestone && task.Id == dep.DependentTask);
                        return dep.DependenceOnTask == task.Id;
                    })!.DependentTask)!.Id,
                    Alias = _dal.Task!.Read(_dal.Dependency!.Read(dep =>
                    {
                        _dal.Task!.Read(task => task.Milestone && task.Id == dep.DependentTask);
                        return dep.DependenceOnTask == task.Id;
                    })!.DependentTask)?.Alias
                },
                RequiredEffortTime = task.RequiredEffortTime,
                ScheduledDate = task.ScheduledDate,
                StartDate = task.StartDate,
                ForecastDate = task.StartDate+task.RequiredEffortTime,
                DeadlineDate = task.DeadlineDate,
                CompleteDate = task.CompletionDate,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                ComplexityTask = (BO.EngineerExperience)task.ComplexityTask!,
                Engineer = new EngineerInTask()
                {
                    Id = (int)task.EngineerId!,
                    Name = _dal.Engineer.Read(id)!.Name
                }
            };

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist",ex);
        }
    }
    public int AddTask(BO.Task task)
    {
        DO.Task doTask = new DO.Task(
            task.Id,
            task.Description!,
            task.Alias!,
            false,
            task.Deliverables!,
            (DO.EngineerExperience)task.ComplexityTask,
            task.RequiredEffortTime,
            task.CreatedAtDate,
            task.StartDate,
            task.ScheduledDate,
            task.DeadlineDate,
            task.CompleteDate,
            task.Remarks,
            task.Engineer!.Id);
        try
        {
            int idNewTask = _dal.Task.Create(doTask);
            return idNewTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists",ex)
    ;
        }
    }
    public void RemoveTask(int id)
    {
        DO.Task? task = _dal.Task.Read(id);
        if (task != null)
        {
            _dal.Task.Delete(id);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
    }
    public void UpdateTask(BO.Task task)
    {
        DO.Task? doTask = _dal.Task.Read(task.Id);
        if (doTask != null)
        {
            _dal.Task.Update(doTask);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={task.Id} does Not exist");
        }
    }


}

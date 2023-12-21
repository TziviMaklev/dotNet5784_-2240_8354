
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEnumerable<BO.TaskInList>? RequestTaskList(Func<BO.TaskInList, bool>? filter)
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
            /*var dependencies = (from DO.Dependency doDependency in _dal.Dependency.ReadAll()
                                where doDependency.DependentTask == id
                                select doDependency.DependenceOnTask).ToList()
                                .ForEach(dep => dependencies.Add(dep));*/
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
                /*Dependencies =*/


        };

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
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
            task.CreatedAtDate,
            task.StartDate,
            task.EstimatedStartDate,
            task.ForecastDate,
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
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex)
    ;
        }
    }
    public void RemoveTask(int id)
    {

    }
    public void UpdateTask(BO.Task task)
    {

    }


}

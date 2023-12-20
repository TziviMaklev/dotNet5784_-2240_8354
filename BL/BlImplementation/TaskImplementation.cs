
namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public void AddTask(Task task)
    {
        DO.Task doTask = new DO.Task(task.id, task.description,task.alias,task.estimatedCompletionDate,task.finalDateForCompletion,task.remarks);
    /*    int Id,
    string Description,
    string Alias,
    bool Milestone,
    string Deliverables,
    EngineerExperience ComplexityTask,
    DateTime CreationDate,
    DateTime? StartDate,
    DateTime? ScheduledDate = null,
    DateTime? ForecastDate = null,
    DateTime? DeadlineDate = null,
    int? EngineerId = null*/

    }

    public void AddTask(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public Task GetTask(int id)
    {
        
    }

    public void RemoveTask(int id)
    {
        
    }

    public IEnumerable<Task>? RequestTaskList(Func<Task, bool> filter)
    {
        
    }

    public IEnumerable<BO.Task>? RequestTaskList(Func<BO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public void UpdateTask(Task task)
    {
        
    }

    public void UpdateTask(BO.Task task)
    {
        throw new NotImplementedException();
    }

    BO.Task ITask.GetTask(int id)
    {
        throw new NotImplementedException();
    }
}



namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public void AddTask(Task task)
    {
        throw new NotImplementedException();
    }

    public Task GetTask(int id)
    {
        throw new NotImplementedException();
    }

    public void RemoveTask(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task>? RequestTaskList(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public void UpdateTask(Task task)
    {
        throw new NotImplementedException();
    }
}

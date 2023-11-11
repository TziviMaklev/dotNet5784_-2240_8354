

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// class for woking with the list of the Tasks
/// </summary>
internal class TaskImplementation : ITask
{
    /// <summary>
    /// creates a new task in the list of tasks
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>returns the id of the task that created</returns>
    public int Create(Task item)
    {
        int id;
        Task? taskFound = DataSource.Tasks.Find(x => x!.Id == item.Id);
        if (taskFound == null)
            id = item.Id;
        else
            id = DataSource.Config.NextIdTask;

        Task newItem = new Task()
        {
            Id = id,
            Description = item.Description,
            Alias=item.Alias,
            Milestone = item.Milestone,
            Deliverables = item.Deliverables,
            ComplexityTask = item.ComplexityTask,
            CreationDate = item.CreationDate,
            StartDate = item.StartDate,
            ScheduledDate = item.ScheduledDate,
            ForecastDate=item.ForecastDate,
            DeadlineDate = item.DeadlineDate,
            CompletionDate = item.CompletionDate,
            Remarks= item.Remarks,
            EngineerId = item.EngineerId,
        };
        List<Task> newTasks = DataSource.Tasks.Append(newItem).ToList()!;
       // DataSource.Tasks = newTasks;
        return id;
    }

    /// <summary>
    /// delete a task from the tasks list
    /// </summary>
    /// <param name="id">the id of the task to delete</param>
    /// <exception cref="Exception">the object is not exist</exception>
    public void Delete(int id)
    {
        Task? taskFound = DataSource.Tasks.Find(x=> x!.Id== id);
        if(taskFound == null) {
            throw new Exception($"Task with ID={id} does Not exist");
        }
        DataSource.Tasks.Remove(taskFound);
    }

    /// <summary>
    /// read a task from the task list
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns>return a reference to the task</returns>
    public Task? Read(int id)
    {
        Task? taskFound = DataSource.Tasks.Find(x => x!.Id == id);
        if (taskFound == null)
        {
            return null;
        }
        return taskFound;
    }

    /// <summary>
    /// read all list of tasks
    /// </summary>
    /// <returns>return a new list that the same as the one exist</returns>
    public List<Task> ReadAll()
    {
        return new List <Task> (DataSource.Tasks!);
    }

    /// <summary>
    /// delete one task from the list and add one with the same id
    /// </summary>
    /// <param name="item">the new item to update</param>
    public void Update(Task item)
    {
        Delete(item.Id);
        Create(item);
    }

    /// <summary>
    /// delete all items in the Task list
    /// </summary>
    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
}

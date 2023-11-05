

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    // creat mew task item
    public int Create(Task item)
    {
        int id = DataSource.Config.NextIdTask;
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
        DataSource.Tasks.Add(newItem);
        return id;
    }

    public void Delete(int id)
    {
        Task taskFound = DataSource.Tasks.Find(x=> x.Id== id);
        if(taskFound == null) {
            throw new Exception("Error , this item is not Exist");
                }
        DataSource.Tasks.Remove(taskFound);
    }

    public Task? Read(int id)
    {
        Task taskFound = DataSource.Tasks.Find(x => x.Id == id);
        if (taskFound == null)
        {
            throw new Exception("Error , this item is not Exist");
        }
        return taskFound;
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        Task taskFound = DataSource.Tasks.Find(x => x.Id == item.Id);
        if (taskFound == null)
        {
            throw new Exception("Error , this item is not Exist");
        }
        taskFound = item;
    }
}

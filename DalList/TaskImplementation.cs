

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
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

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}

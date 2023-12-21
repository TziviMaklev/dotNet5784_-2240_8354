
namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Alias { get; init; }
    public string? Description { get; init; }
    public DateTime CreatedAtDate { get; init; }
    public Status Status { get;set; }
    public List<TaskInList>? Dependencies { get; set; }
    public BO.MilstoneInTask ?Milstone {  get; set; }
    public TimeSpan RequiredEffortTime { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime ?StartDate { get; set; }
    public DateTime ?ForecastDate { get; set; }
    public DateTime ?DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables{ get; set; }
    public string? Remarks { get; set; }
    public EngineerExperience ComplexityTask { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }

    
}


namespace BO;

public class Task
{
    internal int Id { get; init; }
    internal string? Alias { get; init; }
    internal string? Description { get; init; }
    internal DateTime CreatedAtDate { get; init; }
    internal Status Status { get;set; }
    internal List<TaskInList>? Dependencies { get; set; }
    internal BO.MilstoneInTask ?Milstone {  get; set; }
    internal TimeSpan? RequiredEffortTime { get; set; }
    internal DateTime? EstimatedStartDate { get; set; }
    internal DateTime ?StartDate { get; set; }
    internal DateTime ?ForecastDate { get; set; }
    internal DateTime ?DeadlineDate { get; set; }
    internal DateTime? CompleteDate { get; set; }
    internal string? Deliverables{ get; set; }
    internal string? Remarks { get; set; }
    internal EngineerExperience ComplexityTask { get; set; }
    internal BO.EngineerInTask? Engineer { get; set; }

    
}

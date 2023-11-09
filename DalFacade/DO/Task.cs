

using System.Diagnostics.CodeAnalysis;

namespace DO;
/// <summary>
/// An entitety of a task with all the properties it needs
/// </summary>
/// <param name="Id">a running number- different one for any single task</param>
/// <param name="Description">the description of the tast</param>
/// <param name="Alias">a name/alias for the task</param>
/// <param name="Milestone">kind of steps in the task</param>
/// <param name="CreationDate">the day the task created</param>
/// <param name="StartDate">the date the task began done</param>
/// <param name="ScheduledDate">the date you think the task will finished (first one)</param>
/// <param name="ForecastDate">the date you think the task will be done (apdatad)</param>
/// <param name="DeadlineDate">last date for ending the task</param>
/// <param name="CompletionDate">the date the task had realy done</param>
/// <param name="Deliverables">what came out from this task?</param>
/// <param name="Remarks"></param>
/// <param name="EngineerId">the id of who did thos task</param>
/// <param name="ComplexityTask"></param>

public record Task
(
    int Id,
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
    DateTime? CompletionDate = null,
    string? Remarks = null,
    int? EngineerId = null
)
{
    public Task() : this(0,"","",false,"", 0,DateTime.Now,DateTime.Now) { } 
    //empty ctor for stage 3

}


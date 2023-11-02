
namespace DO;

public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime CreationDate,
    DateTime StartDate,
    DateTime? ScheduledDate,
    DateTime? ForecastDate,
    DateTime? DeadlineDate,
    DateTime? CompletionDate,
    string Deliverables = "",
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience complexityTask
);

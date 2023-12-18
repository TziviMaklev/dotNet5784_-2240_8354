
namespace BO;

internal class Task
{
    int Id { get; init; }
    string? Alias { get; init; }
    string? Description { get; init; }
    DateTime ProductionDate { get; init; }
    Status Status { get;set; }
    TaskInList? DependencyList { get; set; }
    DateTime EstimatedStartDate { get; set; }
    DateTime ActualStartDate { get; set; }
    DateTime EstimatedCompletionDate { get; set; }
    DateTime FinalDateForCompletion { get; set; }
    DateTime ActualEndDate { get; set; }
    string? Product { get; set; }
    string? Remarks { get; set; }
    EngineerExperience Difficulty { get; set; }
}

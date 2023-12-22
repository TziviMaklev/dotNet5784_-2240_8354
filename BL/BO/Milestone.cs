
namespace BO;

public class Milestone
{
    int Id { get; init; }
    string? Alias {  get; init; } 
    string? Description {  get; init; }
    DateTime? ProductionDate {  get; init; }
    Status Status{ get; set; }
    DateTime? StartDate { get; set; }
    DateTime? EstimatedCompletionDate {  get; set; }
    DateTime? FinalDateForCompletion { get; set; }
    DateTime? ActualEndDate { get; set; }
    float ProgressPercentage {  get; set; }
    string? Remarks { get; set; }
    TaskInList? DependencyList { get; set; }
}

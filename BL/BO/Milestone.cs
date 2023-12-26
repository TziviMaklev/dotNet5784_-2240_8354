
namespace BO;

public class Milestone
{
    internal int Id { get; init; }
    internal string? Alias {  get; init; } 
    internal string? Description {  get; init; }
    internal DateTime? CreateDate {  get; init; }
    internal Status Status { get; set; }
    internal DateTime? StartDate { get; set; }
    internal DateTime? ForecastDate {  get; set; }
    internal DateTime? Deadline { get; set; }
    internal DateTime? ActualEndDate { get; set; }
    internal float ProgressPercentage {  get; set; }
    internal string? Remarks { get; set; }
    internal TaskInList? DependencyList { get; set; }
}


namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Alias {  get; init; }
    public string? Description {  get; init; }
    public DateTime? CreateDate {  get; init; }
    public Status Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ForecastDate {  get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public float ProgressPercentage {  get; set; }
    public string? Remarks { get; set; }
    public TaskInList? DependencyList { get; set; }
}

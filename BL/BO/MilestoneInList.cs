
namespace BO;

public class MilestoneInList
{
    string? Description { get; init; }
    string? Alias { get; init; }
    DateTime? CreateDate { get; set; }
    Status? Status { get;set; }
    float ProgressPercentage { get; set; }
}

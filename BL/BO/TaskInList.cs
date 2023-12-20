
namespace BO;

public class TaskInList
{
    internal int Id { get; init; }
    internal string? Alias { get; init; }
    internal string? Description { get; init; }
    internal Status Status { get; set; } 
}

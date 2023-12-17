
namespace BO;

internal class TaskInList
{
    int Id { get; init; }
    string? Alias { get; init; }
    string? Description { get; init; }
    Status Status { get; set; }
}

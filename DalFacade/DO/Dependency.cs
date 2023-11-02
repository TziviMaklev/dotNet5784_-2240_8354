
namespace DO;
/// <summary>
/// tasks that depend one on the other
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTask">the depend task</param>
/// <param name="DependenceOnTask">the depend task depends on this</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependenceOnTask
)
{
    public Dependency() : this(0, 0, 0) { }
    //empty ctor for stage 3
}

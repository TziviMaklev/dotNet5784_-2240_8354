

namespace DalApi;

public interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }
    DateTime? StartProject { get; init; }
    DateTime? EndProject { get; init; }
}



namespace DalApi;

internal interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }

}


namespace BlApi;

/// <summary>
/// logic interface that has the whole interfaces in that
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITask Task { get; }
    public void InitializeDB();
}

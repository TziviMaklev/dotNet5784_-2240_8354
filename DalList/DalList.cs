namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IDependency Dependency => new DependecyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    public DateTime? StartProject { get; init; }
    public DateTime? EndProject { get; init; }
}



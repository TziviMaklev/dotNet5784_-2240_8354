

namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    public DateTime? StartProject { get; set; }
    public DateTime? EndProject { get; set; }
}

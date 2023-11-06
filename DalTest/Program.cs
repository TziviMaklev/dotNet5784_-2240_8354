using Dal;
using DalApi;
namespace DalTest;
internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Hello, World!");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
        }
       

    }

    private static ITask ? s_dalTask = new TaskImplementation(); 
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); 
    private static IDependency? s_dalDependency = new DependecyImplementation();

}
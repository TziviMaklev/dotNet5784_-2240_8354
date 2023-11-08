using Dal;
using DalApi;
namespace DalTest;
internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static IDependency? s_dalDependency = new DependecyImplementation();

    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalTask, s_dalDependency, s_dalEngineer);
            Console.WriteLine("Hello, World!");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
        }
       

    }



}
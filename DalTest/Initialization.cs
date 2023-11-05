using DalApi;
using DO;
namespace DalTest;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;

    //use for making random numbers for the objects
    private static readonly Random s_rand = new();

    private static void createTask()
    {

    }
}

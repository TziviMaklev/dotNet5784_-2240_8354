using Dal;
using DalApi;
using DO;

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
            int userChose;
            do
            {
                Console.WriteLine("Select an entity you want to check\n" +
                    "0. EXIT\n" +
                    "1. Task\n" +
                    "2. Engineer\n" +
                    "3. Dependency\n");
                userChose = Convert.ToInt32(Console.ReadLine());//ReadLine returns only string so we converted it to integer
                switch (userChose)
                {
                    case 0:
                        break;
                    case 1:
                        TaskMethod();
                        break;
                    case 2:
                        EngineerMethod();
                        break;
                    case 3:
                        DependencyMethod();
                        break;
                    default:
                        throw new Exception("This choice does not exist");
                }
            } while (userChose != 0);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
        }
       
    }

    internal static void printMenu()
    {
        Console.WriteLine("Select the method you want to perform\n" +
            "1. EXIT\n" +
            "2. Add\n" +
            "3. Object view\n" +
            "4. List view\n" +
            "5. Update\n" +
            "6. Delete\n");
    }
    internal static void TaskMethod()
    {
        printMenu();
        int userChose = Convert.ToInt32(Console.ReadLine());
        switch (userChose)
        {
            case 1:
                return;
            case 2:
                {
                    Console.WriteLine("Enter task description: ");
                    string desc = Console.ReadLine() ?? throw new NullReferenceException();
                    Console.WriteLine("Enter task alias: ");
                    string alias = Console.ReadLine() ?? throw new NullReferenceException();
                    Console.WriteLine("Enter task deliverables: ");
                    string deliver = Console.ReadLine()?? throw new NullReferenceException();
                    Console.WriteLine("Enter the engineer expirience (0-4): ");
                    int engineerExperience = Convert.ToInt32(Console.ReadLine());
                    DO.EngineerExperience experience;
                    switch (engineerExperience)
                    {
                        case 0:
                            experience = EngineerExperience.Novice; break;
                        case 1:
                            experience = EngineerExperience.AdvancedBeginner; break;
                        case 2:
                            experience = EngineerExperience.Competent; break;
                        case 3:
                            experience = EngineerExperience.Proficient; break;
                        case 4:
                            experience = EngineerExperience.Expert; break;
                        default:
                            throw new Exception("This choice does not exist");
                    }
                    DO.Task task = new DO.Task(0,desc,alias,false,deliver,experience
                        ,DateTime.Now,null);
                    s_dalTask.Create(task);
                }
                break;
            case 3:
                {
                    Console.WriteLine("Enter the id of the Task to show: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DO.Task task = s_dalTask.Read(id)!;
                    Console.WriteLine(task);
                }
                break;
            case 4:
                {
                    List<DO.Task?> tasks;
                    tasks = s_dalTask.ReadAll();
                    Console.WriteLine(string.Join("\n", tasks));
                }
                break;
            case 5:
                {

                }

        }
    }

    internal static void EngineerMethod()
    {
        printMenu();
    }

    internal static void DependencyMethod()
    {
        printMenu();
    }
}
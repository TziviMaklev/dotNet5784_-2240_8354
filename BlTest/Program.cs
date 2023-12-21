// See https://aka.ms/new-console-template for more information
using BlApi;
using BO;
using System.Transactions;

internal class Program

{
    static readonly IBl s_bl = BlApi.Factory.Get();
    private static void Main(string[] args)
    {

        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            int userChose;
            do
            {
                Console.WriteLine("Select an entity you want to check\n" +
                    "0. EXIT\n" +
                    "1. Task\n" +
                    "2. Engineer\n" +
                    "3. Milestone\n");
                ans = Console.ReadLine();
                int.TryParse(ans, out userChose);
                switch (userChose)
                {
                    case 0:
                        return;
                    case 1:
                        TaskMethod();
                        break;
                    case 2:
                        EngineerMethod();
                        break;
                    case 3:
                        MilestoneMethod();
                        break;
                    default:
                        throw new ChoiseDoesNotExistException("This choice does not exist");
                }
            } while (userChose != 0);

        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    internal static void printMenu() {
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. List view\n" +
                          "2. Object view\n" +
                          "3. Add\n" +
                          "4. Delete\n" +
                          "5. Update\n");
    }
    internal static void TaskMethod() {
        printMenu();
        string? ans = Console.ReadLine();
        int userChose;
        int.TryParse(ans, out userChose);
        switch (userChose)
        {
            case 0:
                return;
            case 1:
                Console.WriteLine("Enter task description: ");
                string desc = Console.ReadLine() ?? throw new NullReferenceException();
                Console.WriteLine("Enter task alias: ");
                string alias = Console.ReadLine() ?? throw new NullReferenceException();
                Console.WriteLine("Enter task deliverables: ");
                string deliver = Console.ReadLine() ?? throw new NullReferenceException();
                Console.WriteLine("Enter the engineer expirience (0-4): ");
                int engineerExperience = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Does this task have a dependency? 0/1");
                ans= Console.ReadLine();
                int haveDependency;
                int.TryParse(ans, out haveDependency);
                List<int> depps = new List<int>();
                 while (haveDependency == 1)
                {
                    Console.WriteLine("enter ID of dependency:");
                    ans = Console.ReadLine();
                    int.TryParse(ans, out haveDependency);
                    if(ans != null)
                    {
                        depps.Add(ans);
                    }
                   
                }


                EngineerExperience experience;
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
                        throw new ChoiseDoesNotExistException("This choice does not exist");
                }
                BO.Task task = new BO.Task() 
                {
                    Id= 0,
                    Alias = alias,
                    Description = desc,
                    CreatedAtDate = DateTime.Now,
                    Status = 0,
                    Dependencies = 
                    Deliverables = deliver,
                    ComplexityTask= experience, 
                    RequiredEffortTime= TimeSpan.FromDays(0),
                    
                    StartDate=null,
                };
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }
    }
    internal static void EngineerMethod() {
        printMenu();
        string? ans = Console.ReadLine();
        int userChose;
        int.TryParse(ans, out userChose);
        switch (userChose) { 
            case 0:
                 return;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }

    }
    internal static void MilestoneMethod() {
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. creating yhe milestone project schedule \n" +
                          "2. request milestone details\n" +
                          "3. Update\n");
        string? ans = Console.ReadLine();
        int userChose;
        int.TryParse(ans, out userChose);
        switch (userChose)
        {
            case 0:
                return;
            case 1:

                break;
            case 2:
                break;
            case 3:
                break;
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }
    }
}

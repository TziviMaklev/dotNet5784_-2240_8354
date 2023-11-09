﻿using Dal;
using DalApi;
using DO;
using System;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            string ans;
            do
            {
                Console.WriteLine("Select an entity you want to check\n" +
                    "0. EXIT\n" +
                    "1. Task\n" +
                    "2. Engineer\n" +
                    "3. Dependency\n");
                ans = Console.ReadLine();
                int.TryParse(ans, out userChose); ;//ReadLine returns only string so we converted it to integer
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
        string? ans = Console.ReadLine();
        int userChose;
        int.TryParse(ans, out userChose);
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
                    string deliver = Console.ReadLine() ?? throw new NullReferenceException();
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
                    DO.Task task = new DO.Task(0, desc, alias, false, deliver, experience
                        , DateTime.Now, null);
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
                    Console.WriteLine("enter task id:");
                    int id= Convert.ToInt32(Console.ReadLine());
                    DO.Task taskFound =  s_dalTask.Read(id);
                    if (taskFound == null)
                    {
                        throw new Exception($"Task with ID={id} does Not exist");
                    }

                    Console.WriteLine("if you want change the description enter:");
                    string description = Console.ReadLine();
                    if(description == "") 
                        description=taskFound.Description;

                    Console.WriteLine("if you want change the alias enter:");
                    string alias = Console.ReadLine();
                    if (alias == "") 
                        alias = taskFound.Alias;

                    Console.WriteLine("if you want change the milestone enter:");
                    string milestoneI = Console.ReadLine();
                    bool milestone;
                    if (milestoneI == "")
                        milestone = taskFound.Milestone;
                    else
                        milestone= milestoneI == "false" ?  false : true;

                    Console.WriteLine("if you want change the deliverables enter:");
                    string deliverables = Console.ReadLine();
                    if (deliverables == "") 
                        deliverables = taskFound.Deliverables;

                    Console.WriteLine("if you want change the complexityTask enter:(0-4)");
                    int engineerExperience;
                    ans = Console.ReadLine();
                    int.TryParse(ans, out engineerExperience);
                    DO.EngineerExperience complexityTask;
                    if (engineerExperience == null)
                        complexityTask = taskFound.ComplexityTask;
                    else
                        switch (engineerExperience)
                        {
                            case 0:
                                complexityTask = EngineerExperience.Novice; break;
                            case 1:
                                complexityTask = EngineerExperience.AdvancedBeginner; break;
                            case 2:
                                complexityTask = EngineerExperience.Competent; break;
                            case 3:
                                complexityTask = EngineerExperience.Proficient; break;
                            case 4:
                                complexityTask = EngineerExperience.Expert; break;
                            default:
                                throw new Exception("This choice does not exist");
                        }



                    Console.WriteLine("if you want change the creationDate enter:");
                    DateTime creationDate;
                    ans = Console.ReadLine();
                    DateTime.TryParse(ans, out creationDate);
                    if (creationDate == null)
                        creationDate = taskFound.CreationDate;

                    Console.WriteLine("if you want change the startDate enter:");
                    DateTime? startDate = taskFound.StartDate;
                    ans = Console.ReadLine();
                    if (ans != "")
                        startDate = Convert.ToDateTime(ans);

                    Console.WriteLine("if you want change the scheduledDate enter:");
                    DateTime? scheduledDate = taskFound.ScheduledDate;
                    ans = Console.ReadLine();
                    if (ans != "") 
                        scheduledDate = Convert.ToDateTime(ans);

                    Console.WriteLine("if you want change the forecastDate enter:");
                    DateTime? forecastDate = taskFound.ForecastDate;
                    ans = Console.ReadLine();
                    if (ans != "")
                        forecastDate = Convert.ToDateTime(ans);

                    Console.WriteLine("if you want change the deadLineDate enter:");
                    DateTime? deadlineDate = taskFound.DeadlineDate;
                    ans = Console.ReadLine();
                    if (ans != "")
                        deadlineDate = Convert.ToDateTime(ans);


                    Console.WriteLine("if you want change the completionDate enter:");
                    DateTime? completionDate = taskFound.CompletionDate;
                    ans = Console.ReadLine();
                    if (ans != "")
                        completionDate = Convert.ToDateTime(ans);

                    Console.WriteLine("if you want change the remarks enter:");
                    string? remarks = Console.ReadLine();
                    if (remarks == "")
                         remarks = taskFound.Remarks;

                    Console.WriteLine("if you want change the engineerId enter:");
                    int? engineerId = taskFound.EngineerId;
                    ans = Console.ReadLine();
                    if (ans != "")
                        engineerId = Convert.ToInt32(ans);
                    DO.Task task = new DO.Task(taskFound.Id, description ,alias , milestone ,
                        deliverables,complexityTask , creationDate,startDate,scheduledDate,
                        forecastDate,deadlineDate,completionDate,remarks,engineerId);
                    Console.WriteLine(task.Id);
                    s_dalTask.Update(task);
                }

                break;
            case 6:
                {
                    Console.WriteLine("enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    s_dalTask.Delete(id);

                }
                break;
            default:
                throw new Exception("This choice does not exist");

        }
    }

    internal static void EngineerMethod()
    {
        printMenu();
        int userChose = Convert.ToInt32(Console.ReadLine());
        switch (userChose)
        {
            case 1:
                {
                    return;
                }
            case 2:
                {
                    Console.WriteLine("enter engineer id");
                    int id = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
                    Console.WriteLine("enter engineer name");
                    string name = Console.ReadLine() ?? throw new NullReferenceException();
                    Console.WriteLine("enter engineer email");
                    string email = Console.ReadLine() ?? throw new NullReferenceException();
                    Console.WriteLine("enter engineer engineerExperience");
                    int engineerExperience = Convert.ToInt32(Console.ReadLine());
                    DO.EngineerExperience level;
                    Console.WriteLine("enter engineer cost");
                    double cost = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

                    switch (engineerExperience)
                    {
                        case 0:
                            level = EngineerExperience.Novice; break;
                        case 1:
                            level = EngineerExperience.AdvancedBeginner; break;
                        case 2:
                            level = EngineerExperience.Competent; break;
                        case 3:
                            level = EngineerExperience.Proficient; break;
                        case 4:
                            level = EngineerExperience.Expert; break;
                        default:
                            throw new Exception("This choice does not exist");
                    }
                    DO.Engineer engineer = new DO.Engineer(id, name, email, level, cost);
                    s_dalEngineer.Create(engineer);
                }
                break;
            case 3:
                {
                    Console.WriteLine("Enter the id of the engineer to show: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DO.Engineer engineer = s_dalEngineer.Read(id)!;
                    Console.WriteLine(engineer);
                }
                break;
            case 4:
                {
                    List<DO.Engineer?> engineerL;
                    engineerL = s_dalEngineer.ReadAll();
                    Console.WriteLine(string.Join("\n", engineerL));
                }
                break;
            case 5:
                {
                    Console.WriteLine("enter task id:");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    DO.Engineer engineerFound = s_dalEngineer.Read(id);
                    if (engineerFound == null)
                    {
                        throw new Exception($"Task with ID={id} does Not exist");
                    }
                    Console.WriteLine("if you want change the name enter:");
                    string? name = Console.ReadLine();
                    if(name  == "")
                        name = engineerFound.Name;

                    Console.WriteLine("if you want change the email enter:");
                    string? email = Console.ReadLine();
                    if (email == "")
                        email = engineerFound.Email;

                    Console.WriteLine("if you want change the level enter:");
                    int engineerExperience;
                    int.TryParse(Console.ReadLine(), out engineerExperience);
                    DO.EngineerExperience level;
                    if (engineerExperience == null)
                        level = engineerFound.Level;
                    else
                        switch (engineerExperience)
                        {
                            case 0:
                                level = EngineerExperience.Novice; break;
                            case 1:
                                level = EngineerExperience.AdvancedBeginner; break;
                            case 2:
                                level = EngineerExperience.Competent; break;
                            case 3:
                                level = EngineerExperience.Proficient; break;
                            case 4:
                                level = EngineerExperience.Expert; break;
                            default:
                                throw new Exception("This choice does not exist");
                        }
                    Console.WriteLine("if you want change the cost enter:");
                    double cost;
                    double.TryParse(Console.ReadLine(), out cost);
                    DO.Engineer engineer = new DO.Engineer(engineerFound.Id, name, email , level , cost);
                    s_dalEngineer.Update(engineer);
                }
                break;
            case 6:
                {
                    Console.WriteLine("enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    s_dalEngineer.Delete(id);
                }
                break;
            default:
                throw new Exception("This choice does not exist");

        }
    }

    internal static void DependencyMethod()
    {
        printMenu();
        int userChose = Convert.ToInt32(Console.ReadLine());
        switch (userChose)
        {
            case 1:
                {
                    return;
                }
            case 2:
                {
                    Console.WriteLine("enter dependency id");
                    int id = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
                    Console.WriteLine("enter dependency dependentTask");
                    int dependentTask = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
                    Console.WriteLine("enter dependency dependenceOnTask");
                    int dependenceOnTask = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
                    DO.Dependency dependency = new DO.Dependency(id, dependentTask, dependenceOnTask);
                    s_dalDependency.Create(dependency);
                }
                break;
            case 3:
                {
                    Console.WriteLine("Enter the id of the dependency to show: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DO.Dependency dependency = s_dalDependency.Read(id)!;
                    Console.WriteLine(dependency);
                }
                break;
            case 4:
                {
                    List<DO.Dependency?> dependencyL;
                    dependencyL = s_dalDependency.ReadAll();
                    Console.WriteLine(string.Join("\n", dependencyL));
                }
                break;
            case 5:
                {
                    Console.WriteLine("enter dependency id");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DO.Dependency dependencyFound = s_dalDependency.Read(id);
                    if (dependencyFound == null)
                    {
                        throw new Exception($"Task with ID={id} does Not exist");
                    }
                    Console.WriteLine("if you want change the dependentTask enter:");
                    int dependentTask = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("if you want change the dependenceOnTask enter:");
                    int dependenceOnTask = Convert.ToInt32(Console.ReadLine());
                    DO.Dependency dependency = new DO.Dependency(dependencyFound.Id, dependentTask, dependenceOnTask);
                    s_dalDependency.Update(dependency);
                }
                break;
            case 6:
                {
                    Console.WriteLine("enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    s_dalDependency.Delete(id);
                }
                break;
            default:
                throw new Exception("This choice does not exist");

        }
    }
}
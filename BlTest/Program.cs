﻿using BlApi;
using BO;




internal class Program

{
    static readonly IBl s_bl = BlApi.Factory.Get;
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
    internal static void printMenu()
    {
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. List view\n" +
                          "2. Object view\n" +
                          "3. Add\n" +
                          "4. Delete\n" +
                          "5. Update\n");
    }
    internal static void TaskMethod()
    {
        printMenu();
        string? ans = Console.ReadLine();
        int userChose, id;
        bool succesfull;
        int.TryParse(ans, out userChose);
        switch (userChose)
        {
            case 0:
                return;
            case 1:
                {
                    IEnumerable<BO.TaskInList?> tasks;
                    tasks = s_bl.Task!.RequestTaskList()!.ToList();
                    Console.WriteLine(string.Join("\n", tasks));
                }
                break;
            case 2:
                Console.WriteLine("Enter the id of the Task to show: ");
                ans = Console.ReadLine();
                succesfull = int.TryParse(ans, out id);
                if (succesfull)
                {
                    BO.Task task1 = s_bl!.Task.GetTask(id)!;
                    Console.WriteLine(task1);
                }
                break;
            case 3:
                Console.WriteLine("Enter task description: ");
                string? desc = Console.ReadLine() ?? null;
                Console.WriteLine("Enter task alias: ");
                string? alias = Console.ReadLine() ?? null;
                Console.WriteLine("Enter task deliverables: ");
                string? deliver = Console.ReadLine() ?? null;
                Console.WriteLine("Enter task remarks: ");
                string? remarks = Console.ReadLine() ?? null;
                Console.WriteLine("Enter the engineer expirience (0-4): ");
                int engineerExperience = Convert.ToInt32(Console.ReadLine());
                List<TaskInList> depps = new List<TaskInList>();
                int haveDependency;
                do
                {
                    Console.WriteLine("Does this task have to add a dependency? 0/1");
                    ans = Console.ReadLine();
                    int.TryParse(ans, out haveDependency);
                    if (haveDependency == 1)
                    {
                        Console.WriteLine("enter ID of dependency:");
                        ans = Console.ReadLine();
                        succesfull = int.TryParse(ans, out id);
                        if (succesfull)
                        {
                            depps.Add(new TaskInList()
                            {
                                Id = id,
                                Status = 0,
                            });
                        }
                    }

                } while (haveDependency == 1);
                BO.EngineerExperience experience;
                switch (engineerExperience)
                {
                    case 0:
                        experience = BO.EngineerExperience.Novice; break;
                    case 1:
                        experience = BO.EngineerExperience.AdvancedBeginner; break;
                    case 2:
                        experience = BO.EngineerExperience.Competent; break;
                    case 3:
                        experience = BO.EngineerExperience.Proficient; break;
                    case 4:
                        experience = BO.EngineerExperience.Expert; break;
                    default:
                        throw new ChoiseDoesNotExistException("This choice does not exist");
                }
                Console.WriteLine("What is the task duration?");
                ans = Console.ReadLine();
                int taskDuration;
                succesfull = int.TryParse(ans, out taskDuration);
                TimeSpan requiredEffortTime;
                if (succesfull)
                {
                    requiredEffortTime = TimeSpan.FromDays(taskDuration);
                }
                else
                {
                    requiredEffortTime = TimeSpan.FromDays(0);
                }
                BO.Task task = new BO.Task()
                {
                    Id = 0,
                    Alias = alias,
                    Description = desc,
                    CreatedAtDate = DateTime.Now,
                    Status = 0,
                    Dependencies = depps,
                    Milstone = null,
                    RequiredEffortTime = requiredEffortTime,
                    ScheduledDate = null,
                    StartDate = null,
                    ForecastDate = null,
                    DeadlineDate = null,
                    CompleteDate = null,
                    Deliverables = deliver,
                    Remarks = remarks,
                    ComplexityTask = experience,
                    Engineer = null,
                };
                break;
            case 4:
                Console.WriteLine("enter id:");
                ans = Console.ReadLine();
                succesfull = int.TryParse(ans, out id);
                if (succesfull)
                {
                    s_bl!.Task.RemoveTask(id);
                }
                else
                {
                    throw new BlDoesNotExistException("An task with this ID number not exists");
                }
                break;
            case 5:
                Console.WriteLine("enter task id:");
                ans = Console.ReadLine();
                succesfull = int.TryParse(ans, out id);
                BO.Task? taskFound = s_bl!.Task.GetTask(id);
                if (taskFound == null)
                {
                    throw new BlDoesNotExistException($"Task with ID={id} does Not exist");
                }

                Console.WriteLine("if you want change the description enter:");
                string? description = Console.ReadLine() ?? taskFound.Description;

                Console.WriteLine("if you want change the alias enter:");
                alias = Console.ReadLine() ?? taskFound.Alias;

                Console.WriteLine("if you want change the milestone enter:");
                Console.Write("di:");
                BO.MilstoneInTask milestone;
                ans = Console.ReadLine() ?? null;
                if (ans == null)
                {
                    milestone = taskFound.Milstone!;
                }
                else
                {
                    succesfull = int.TryParse(ans, out id);
                    if (succesfull)
                    {
                        Console.Write("alias:");
                        ans = Console.ReadLine() ?? taskFound.Milstone!.Alias;
                        milestone = new MilstoneInTask()
                        {
                            Id = id,
                            Alias = ans
                        };
                    }
                    else
                    {
                        throw new ThePogramNotSuccedToConvert("the convertof  the id not succed");
                    }


                }


                Console.WriteLine("if you want change the deliverables enter:");
                string? deliverables = Console.ReadLine() ?? taskFound.Deliverables;

                Console.WriteLine("if you want change the complexityTask enter:(0-4)");
                ans = Console.ReadLine();
                bool check = int.TryParse(ans, out engineerExperience);
                EngineerExperience complexityTask;
                if (!check)
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
                            throw new ChoiseDoesNotExistException("This choice does not exist");
                    }

                Console.WriteLine("if you want change the RequiredEffortTime enter:");
                ans = Console.ReadLine();
                check = TimeSpan.TryParse(ans, out requiredEffortTime);
                if (!check)
                    requiredEffortTime = taskFound.RequiredEffortTime;

                Console.WriteLine("if you want change the creationDate enter:");
                DateTime creationDate;
                ans = Console.ReadLine();
                check = DateTime.TryParse(ans, out creationDate);
                if (!check)
                    creationDate = taskFound.CreatedAtDate;

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

                Console.WriteLine("if you want change the deadLineDate enter:");
                DateTime? deadlineDate = taskFound.DeadlineDate;
                ans = Console.ReadLine();
                if (ans != "")
                    deadlineDate = Convert.ToDateTime(ans);


                Console.WriteLine("if you want change the completionDate enter:");
                DateTime? completionDate = taskFound.CompleteDate;
                ans = Console.ReadLine();
                if (ans != "")
                    completionDate = Convert.ToDateTime(ans);

                Console.WriteLine("if you want change the remarks enter:");
                remarks = Console.ReadLine();
                if (remarks == "")
                    remarks = taskFound.Remarks;

                Console.WriteLine("if you want change the engineerId enter:");
                BO.EngineerInTask? engineer = taskFound.Engineer;
                Console.WriteLine("id:");
                ans = Console.ReadLine();
                if (ans != "")
                {
                    succesfull = int.TryParse(ans, out id);
                    if (succesfull)
                    {
                        Console.WriteLine("name:");
                        ans = Console.ReadLine() ?? taskFound.Engineer!.Name;
                        engineer = new EngineerInTask()
                        {
                            Id = id,
                            Name = ans
                        };
                    }
                    else
                    {
                        throw new ThePogramNotSuccedToConvert("the convertof  the id not succed");
                    }
                }
                BO.Task boTask = new BO.Task()
                {
                    Id = taskFound.Id,
                    Description = description,
                    Alias = alias,
                    Milstone = milestone,
                    Deliverables = deliverables,
                    ComplexityTask = complexityTask,
                    RequiredEffortTime = requiredEffortTime,
                    CreatedAtDate = creationDate,
                    StartDate = startDate,
                    ScheduledDate = scheduledDate,
                    DeadlineDate = deadlineDate,
                    CompleteDate = completionDate,
                    Remarks = remarks,
                    Engineer = engineer
                };
                Console.WriteLine(boTask.Id);
                s_bl!.Task.UpdateTask(boTask);
                break;
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }
    }
    internal static void EngineerMethod()
    {
        printMenu();
        string? ans = Console.ReadLine();
        int userChose;
        int.TryParse(ans, out userChose);
        switch (userChose)
        {
            case 0:
                return;
            case 1:
                List<BO.Engineer> engineerL = s_bl.Engineer.RequestEngineersList()!.ToList();
                Console.WriteLine(string.Join("\n", engineerL));
                break;
            case 2:
                Console.WriteLine("Enter the id of the engineer to show: ");
                int id = Convert.ToInt32(Console.ReadLine());
                BO.Engineer engineer = s_bl.Engineer?.RequestEngineerDetails(id)!;
                Console.WriteLine(engineer);
                break;
            case 3:
                Console.WriteLine("enter engineer id");
                id = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
                Console.WriteLine("enter engineer name");
                string name = Console.ReadLine() ?? throw new NullReferenceException();
                Console.WriteLine("enter engineer email");
                string email = Console.ReadLine() ?? throw new NullReferenceException();
                Console.WriteLine("enter engineer engineerExperience");
                int engineerExperience = Convert.ToInt32(Console.ReadLine());
                BO.EngineerExperience level;
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
                        throw new ChoiseDoesNotExistException("This choice does not exist");
                }
                Engineer engineer1 = new Engineer()
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    Level = level,
                    Cost = cost
                };
                s_bl.Engineer?.AddEngineer(engineer1);
                break;
            case 4:
                Console.WriteLine("enter id:");
                id = Convert.ToInt32(Console.ReadLine());
                s_bl.Engineer?.RemoveEngineer(id);
                break;
            case 5:
                Console.WriteLine("enter task id:");
                int.TryParse(Console.ReadLine(), out id);

                BO.Engineer engineerFound = s_bl.Engineer?.UpdateEngineerDetails(id)
                    ?? throw new Exception($"Engineer with ID={id} does Not exist");
                Console.WriteLine("if you want change the name enter:");
                name = Console.ReadLine() ?? engineerFound.Name!;

                Console.WriteLine("if you want change the email enter:");
                email = Console.ReadLine() ?? engineerFound.Email!;

                Console.WriteLine("if you want change the level enter:");
                bool check = int.TryParse(Console.ReadLine(), out engineerExperience);
                if (!check)
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
                            throw new ChoiseDoesNotExistException("This choice does not exist");
                    }
                Console.WriteLine("if you want change the cost enter:");
                double.TryParse(Console.ReadLine(), out cost);
                engineer1 = new BO.Engineer()
                {
                    Id = engineerFound.Id,
                    Name = name,
                    Email = email,
                    Level = level,
                    Cost = cost
                };
                s_bl.Engineer.AddEngineer(engineer1);
                break;
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }

    }
    internal static void MilestoneMethod()
    {
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. creating the milestone project schedule \n" +
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

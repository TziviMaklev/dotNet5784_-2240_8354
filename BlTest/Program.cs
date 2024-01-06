using BlApi;
using BO;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program

{
    static readonly IBl s_bl = BlApi.Factory.Get;
    private static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)\n");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            int userChose;
            do
            {
                Console.WriteLine("Select an entity you want to check\n" + // Prompt the user to select an entity they want to check
                    "0. EXIT\n" +
                    "1. Task\n" +
                    "2. Engineer\n" +
                    "3. Milestone\n");
                ans = Console.ReadLine(); // Read the user input and store it in the ans variable
                int.TryParse(ans, out userChose); // Try to parse the value of ans to an integer and store the result in the userChose variable
                switch (userChose) // Switch statement based on the value of userChose
                {
                    case 0: // If userChose is 0
                        return; // Exit the method
                    case 1: // If userChose is 1
                        TaskMethod(); // Call the TaskMethod() method
                        break; // Exit the switch statement
                    case 2: // If userChose is 2
                        EngineerMethod(); // Call the EngineerMethod() method
                        break; // Exit the switch statement
                    case 3: // If userChose is 3
                        MilestoneMethod(); // Call the MilestoneMethod() method
                        break; // Exit the switch statement
                    default: // If userChose does not match any of the above cases
                        throw new ChoiseDoesNotExistException("This choice does not exist"); // Throw a ChoiseDoesNotExistException with the specified message
                }
            } while (userChose != 0); // Continue looping until userChose is 0

        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.ToString());//throw an exeption is there is any error...
        }
    }
    
    /// <summary>
    /// Prints the menu options for the user to select from.
    /// </summary>
    internal static void printMenu()
    {
        // Display the menu options to the user
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. List view\n" +
                          "2. Object view\n" +
                          "3. Add\n" +
                          "4. Delete\n" +
                          "5. Update\n");
    }
    
    /// <summary>
    ///this function is if the user chose to enter the task 
    ///the function print the menu
    ///and do what the user ask
    /// </summary>
    internal static void TaskMethod()
    {
        printMenu();// Print the menu options for the user to select from
        string? ans = Console.ReadLine();// Read the user's input from the console
        int userChose, id;
        bool succesfull;
        int.TryParse(ans, out userChose);// Convert the user's input to an integer
        switch (userChose)
        {
            case 0://Exit
                return;
            case 1: // List view 
                {
                    IEnumerable<BO.TaskInList?> tasks; // Declare a variable to hold a collection of nullable BO.TaskInList objects
                    tasks = s_bl.Task!.RequestTaskList()!.ToList(); // Call the RequestTaskList() method on the s_bl.Task object and convert the result to a list. The '!' operator is used to assert that s_bl.Task is not null.
                    foreach (var item in tasks) // Iterate over each item in the tasks collection
                    {
                        Console.WriteLine(item!.ToString() + '\n'); // Print the string representation of the item to the console. The '!' operator is used to assert that the item is not null.
                    }
                }
                break; // Exit the switch or case statement
            case 2: // Object view
                Console.WriteLine("Enter the ID of the task to show: "); // Prompt the user to enter the ID of the task they want to view
                ans = Console.ReadLine(); // Read the user's input and store it in the 'ans' variable
                succesfull = int.TryParse(ans, out id); // Attempt to parse the user's input into an integer and store the result in the 'id' variable
                if (succesfull) // If the parsing is successful
                {
                    BO.Task task1 = s_bl!.Task.GetTask(id)!; // Retrieve the task with the specified ID using the 'GetTask()' method and store it in the 'task1' variable
                    Console.WriteLine(task1.ToString()); // Print the string representation of the task to the console
                }
                break; // Exit the case block
            case 3://ADD
                Console.WriteLine("Enter task description: ");
                string? desc = Console.ReadLine() ?? null;
                // Prompt the user to enter the task description and assign it to the desc variable.
                // If the user does not enter anything (null), the desc variable will be assigned null.

                Console.WriteLine("Enter task alias: ");
                string? alias = Console.ReadLine() ?? null;
                // Prompt the user to enter the task alias and assign it to the alias variable.
                // If the user does not enter anything (null), the alias variable will be assigned null.

                Console.WriteLine("Enter task deliverables: ");
                string? deliver = Console.ReadLine() ?? null;
                // Prompt the user to enter the task deliverables and assign it to the deliver variable.
                // If the user does not enter anything (null), the deliver variable will be assigned null.

                Console.WriteLine("Enter task remarks: ");
                string? remarks = Console.ReadLine() ?? null;
                // Prompt the user to enter the task remarks and assign it to the remarks variable.
                // If the user does not enter anything (null), the remarks variable will be assigned null.

                Console.WriteLine("Enter the engineer expirience (0-4): ");
                int engineerExperience = Convert.ToInt32(Console.ReadLine());
                // Prompt the user to enter the engineer experience and convert it to an integer.
                // Assign the converted value to the engineerExperience variable.

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
                // Prompt the user to add a dependency to the task.
                // If the user enters 1, prompt for the ID of the dependency and add it to the depps list.

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
                // Create a BO.EngineerExperience variable called experience.
                // Based on the value of engineerExperience, assign the corresponding BO.EngineerExperience enum value to experience.
                // If the value of engineerExperience does not match any case, throw a ChoiseDoesNotExistException.

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
                // Prompt the user to enter the task duration and convert it to an integer.
                // Assign the converted value to the taskDuration variable.
                // If the conversion is successful, create a TimeSpan called requiredEffortTime and set it to the number of days based on taskDuration.
                // If the conversion fails, set requiredEffortTime to 0 days.

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
                // Create a new instance of BO.Task called task and initialize its properties.
                // Assign values to properties based on the variables and input gathered earlier.

                s_bl.Task.AddTask(task); // add the task

                break;
            case 4://DELET
                // Prompt the user to enter the task ID
                Console.WriteLine("enter id:");
                ans = Console.ReadLine();
                succesfull = int.TryParse(ans, out id);
                if (succesfull)
                {
                    s_bl!.Task.RemoveTask(id);
                    // Remove the task with the given ID using the RemoveTask method.
                }
                else
                {
                    throw new BlDoesNotExistException("An task with this ID number not exists");

                }
                break;
            case 5://Update
                // Prompt the user to enter the task ID
                Console.WriteLine("enter task id:");
                ans = Console.ReadLine();
                succesfull = int.TryParse(ans, out id);
                BO.Task? taskFound = s_bl!.Task.GetTask(id);
                // Retrieve the task based on the provided ID using the GetTask method.
                if (taskFound == null)
                {
                    throw new BlDoesNotExistException($"Task with ID={id} does Not exist");
                    // Throw an exception if the task with the given ID does not exist.
                }

                // Prompt the user to change the description
                Console.WriteLine("if you want change the description enter:");
                string? description = Console.ReadLine() ?? taskFound.Description;
                // Read the input from the user and assign it to the description variable.
                // If the user does not enter anything (null), the taskFound.Description value is used.

                // Prompt the user to change the alias
                Console.WriteLine("if you want change the alias enter:");
                string? ansAlias = Console.ReadLine();
                alias = ansAlias != "" ? ansAlias : taskFound.Alias;
                // Read the input from the user and assign it to the alias variable.
                // If the user does not enter anything (empty string), the taskFound.Alias value is used.

                // Prompt the user to change the milestone
                Console.WriteLine("if you want change the milestone enter:");
                Console.Write("id:");
                BO.MilstoneInTask milestone;
                ans = Console.ReadLine();
                if (ans != "")
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
                        // Read the input from the user and create a new MilstoneInTask object with the provided ID and Alias.

                    }
                    else
                    {
                        throw new ThePogramNotSuccedToConvert("the convertof  the id not succed");
                        // Throw an exception if the ID cannot be successfully converted to an integer.
                    }
                }
                else
                {
                    milestone = taskFound.Milstone!;
                }

                // Prompt the user to change the deliverables
                Console.WriteLine("if you want change the deliverables enter:");
                string? deliverables = Console.ReadLine() ?? taskFound.Deliverables;
                // Read the input from the user and assign it to the deliverables variable.
                // If the user does not enter anything (null), the taskFound.Deliverables value is used.

                // Prompt the user to change the complexityTask
                Console.WriteLine("if you want change the complexityTask enter:(0-4)");
                ans = Console.ReadLine();
                bool check = int.TryParse(ans, out engineerExperience);
                EngineerExperience complexityTask;
                if (!check)
                    complexityTask = taskFound.ComplexityTask;
                // If the input cannot be parsed as an integer, use the original complexityTask value.
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
                            // Throw an exception if the input is not a valid choice (0-4).
                    }

                // Prompt the user to change the creationDate date
                Console.WriteLine("if you want change the requiredEffortTime enter:");
                ans = Console.ReadLine();
                check = TimeSpan.TryParse(ans, out requiredEffortTime);// Convert the input string to a TimeSpan object
                if (!check)
                    requiredEffortTime = taskFound.RequiredEffortTime;

                // Prompt the user to change the creationDate date
                Console.WriteLine("if you want change the creationDate enter:");
                DateTime creationDate;
                ans = Console.ReadLine();
                check = DateTime.TryParse(ans, out creationDate);// Convert the input string to a DateTime object
                if (!check)
                    creationDate = taskFound.CreatedAtDate;

                // Prompt the user to change the startDate date
                Console.WriteLine("if you want change the startDate enter:");
                DateTime? startDate = taskFound.StartDate;
                ans = Console.ReadLine();
                if (ans != "")
                    startDate = Convert.ToDateTime(ans);// Convert the input string to a DateTime object

                // Prompt the user to change the scheduledDate date
                Console.WriteLine("if you want change the scheduledDate enter:");
                DateTime? scheduledDate = taskFound.ScheduledDate;
                ans = Console.ReadLine();
                if (ans != "")
                    scheduledDate = Convert.ToDateTime(ans);// Convert the input string to a DateTime object

                // Prompt the user to change the deadline date
                Console.WriteLine("if you want change the deadLineDate enter:");
                DateTime? deadlineDate = taskFound.DeadlineDate;
                ans = Console.ReadLine();
                if (ans != "")
                    deadlineDate = Convert.ToDateTime(ans);// Convert the input string to a DateTime object

                // Prompt the user to change the completion date
                Console.WriteLine("if you want change the completionDate enter:");
                DateTime? completionDate = taskFound.CompleteDate;
                ans = Console.ReadLine();
                if (ans != "")
                    completionDate = Convert.ToDateTime(ans);// Convert the input string to a DateTime object


                // Prompt the user to change the remarks
                Console.WriteLine("if you want change the remarks enter:");
                remarks = Console.ReadLine();
                if (remarks == "")
                    remarks = taskFound.Remarks;// Keep the original remarks if no new input is provided

                // Prompt the user to change the engineer ID
                Console.WriteLine("if you want change the engineerId enter:");
                BO.EngineerInTask? engineer = taskFound.Engineer;
                Console.WriteLine("id:");
                ans = Console.ReadLine();
                if (ans != "")
                {
                    succesfull = int.TryParse(ans, out id);// Try to parse the input as an integer
                        if (succesfull)
                    {
                        Console.WriteLine("name:");
                        ans = Console.ReadLine() ?? taskFound.Engineer!.Name;// Use the original name if no new input is provided
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

                // Create a new Task object with the updated values
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
    
    /// <summary>
    ///this function is if the user chose to enter the   Engineer
    ///the function print the menu
    ///and do what the user ask
    /// </summary>
    internal static void EngineerMethod()
    {
        printMenu();// Print the menu options for the user to select from
        string? ans = Console.ReadLine();// Read the user's input from the console
        int userChose;
        int.TryParse(ans, out userChose);// Convert the user's input to an integer
        switch (userChose)
        {
            case 0://exit
                return;
            case 1: // List view 
                {
                    List<BO.Engineer> engineerL = s_bl.Engineer.RequestEngineersList()!.ToList(); // Declare a list variable to hold a collection of Engineer objects. Call the RequestEngineersList() method on the s_bl.Engineer object and convert the result to a list. The '!' operator is used to assert that s_bl.Engineer is not null.
                    foreach (var item in engineerL) // Iterate over each item in the engineerL list
                    {
                        Console.WriteLine(item.ToString() + '\n'); // Print the string representation of the item to the console
                    }
                    //Console.WriteLine(string.Join("\n", engineerL.ToString())); // Commented out line - It seems to be an attempt to print the entire list as a string, but it is not necessary as the foreach loop is already printing each item.
                }
                break; // Exit the switch or case statement
            case 2: // Object view
                {
                    Console.WriteLine("Enter the id of the engineer to show: "); // Prompt the user to enter the ID of the engineer to view
                    int id = Convert.ToInt32(Console.ReadLine()); // Read the user input and convert it to an integer

                    BO.Engineer engineer = s_bl.Engineer?.RequestEngineerDetails(id)!; // Call the RequestEngineerDetails() method on the s_bl.Engineer object to retrieve the details of the engineer with the given ID. The '!' operator is used to assert that s_bl.Engineer is not null.

                    Console.WriteLine(engineer.ToString()); // Print the string representation of the engineer object to the console
                }
                break; // Exit the switch or case statement
            case 3: // Add
                    Console.WriteLine("enter engineer id"); // Prompt the user to enter the ID of the engineer
                    int id1 = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException()); // Read the user input and convert it to an integer. If the input is null, throw a NullReferenceException.

                    Console.WriteLine("enter engineer name"); // Prompt the user to enter the name of the engineer
                    string name = Console.ReadLine() ?? throw new NullReferenceException(); // Read the user input for the name. If the input is null, throw a NullReferenceException.

                    Console.WriteLine("enter engineer email"); // Prompt the user to enter the email of the engineer
                    string email = Console.ReadLine() ?? throw new NullReferenceException(); // Read the user input for the email. If the input is null, throw a NullReferenceException.

                    Console.WriteLine("enter engineer engineerExperience"); // Prompt the user to enter the engineer experience level
                    int engineerExperience = Convert.ToInt32(Console.ReadLine()); // Read the user input for the engineer experience and convert it to an integer

                    BO.EngineerExperience level; // Declare a variable to hold the engineer experience level

                    Console.WriteLine("enter engineer cost"); // Prompt the user to enter the cost of the engineer
                    double cost = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException()); // Read the user input for the cost. If the input is null, throw a NullReferenceException.

                    switch (engineerExperience) // Use a switch statement to determine the engineer experience level based on the user input
                    {
                        case 0:
                            level = BO.EngineerExperience.Novice; break; // If engineerExperience is 0, set level to Novice
                        case 1:
                            level = BO.EngineerExperience.AdvancedBeginner; break; // If engineerExperience is 1, set level to AdvancedBeginner
                        case 2:
                            level = BO.EngineerExperience.Competent; break; // If engineerExperience is 2, set level to Competent
                        case 3:
                            level = BO.EngineerExperience.Proficient; break; // If engineerExperience is 3, set level to Proficient
                        case 4:
                            level = BO.EngineerExperience.Expert; break; // If engineerExperience is 4, set level to Expert
                    default:
                        throw new ChoiseDoesNotExistException("This choice does not exist"); // If engineerExperience is not 0, 1, 2, 3, or 4, throw a ChoiseDoesNotExistException with the specified message
                }

                Engineer engineer1 = new Engineer() // Create a new instance of the Engineer class and initialize its properties
                {
                    Id = id1, // Set the Id property to the value of id1
                    Name = name, // Set the Name property to the value of name
                    Email = email, // Set the Email property to the value of email
                    Level = level, // Set the Level property to the value of level
                    Cost = cost // Set the Cost property to the value of cost
                };

                s_bl.Engineer?.AddEngineer(engineer1); // Call the AddEngineer() method on the s_bl.Engineer object and pass in the engineer1 object as a parameter. The '?' operator is used to check if s_bl.Engineer is not null before calling the method.
                break; // Exit the switch or case statement
            case 4: // Delete
                {
                    Console.WriteLine("enter id:"); // Prompt the user to enter the ID of the engineer to delete
                    int id2 = Convert.ToInt32(Console.ReadLine()); // Read the user input and convert it to an integer

                    s_bl.Engineer?.RemoveEngineer(id2); // Call the RemoveEngineer() method on the s_bl.Engineer object and pass in the id2 as a parameter. The '?' operator is used to check if s_bl.Engineer is not null before calling the method.
                }
                break; // Exit the switch or case statement
            case 5://update
                Console.WriteLine("enter task id:");// Prompt the user to enter the ID of the engineer to update
                int id3;
                int.TryParse(Console.ReadLine(), out id3);// Read the user input and try to parse it to an integer. Store the result in id2.

                BO.Engineer engineerFound = s_bl.Engineer?.RequestEngineerDetails(id3)// Call the RequestEngineerDetails() method on the s_bl.Engineer object and pass in the id2 as a parameter. The '?' operator is used to check if s_bl.Engineer is not null before calling the method.
                    ?? throw new Exception($"Engineer with ID={id3} does Not exist");// If engineerFound is null, throw an Exception with the specified message.
                
                Console.WriteLine("if you want change the name enter:");
                ans = Console.ReadLine();
                string ? nameE;
                nameE = ans =="" ? engineerFound.Name! : ans;// If ans is an empty string, use the original name of the engineer (engineerFound.Name), otherwise use the value of ans.

                Console.WriteLine("if you want change the email enter:");
                ans = Console.ReadLine();
                string ?emailE;
                emailE = ans == "" ? engineerFound.Email! : ans;// If ans is an empty string, use the original email of the engineer (engineerFound.Email), otherwise use the value of ans.

                Console.WriteLine("if you want change the level enter:");
                bool check = int.TryParse(Console.ReadLine(), out engineerExperience);// Read the user input and try to parse it to an integer. Store the result in engineerExperience.
                if (!check)
                    level = engineerFound.Level;// If the parsing fails, use the original level of the engineer (engineerFound.Level).
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
                Console.WriteLine("if you want change the cost enter:"); // Prompt the user to enter the new cost
                double.TryParse(Console.ReadLine(), out cost); // Read the user input and try to parse it to a double. Store the result in the cost variable.

                engineer1 = new BO.Engineer() // Create a new instance of the BO.Engineer class and initialize its properties
                {
                    Id = engineerFound.Id, // Set the Id property to the value of engineerFound.Id
                    Name = nameE, // Set the Name property to the value of nameE
                    Email = emailE, // Set the Email property to the value of emailE
                    Level = level, // Set the Level property to the value of level
                    Cost = cost // Set the Cost property to the value of cost
                };

                s_bl.Engineer.AddEngineer(engineer1); // Call the AddEngineer() method on the s_bl.Engineer object and pass in the engineer1 object as a parameter
                break; // Exit the switch or case statement
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist"); // If none of the cases match, throw a ChoiseDoesNotExistException with the specified message
        }
    }
    
    /// <summary>
    ///this function is if the user chose to enter the Milestone  
    ///the function print the menu
    ///and do what the user ask
    /// </summary>
    internal static void MilestoneMethod()
    {
        Console.WriteLine("Select the method you want to perform\n" +
                          "0. EXIT\n" +
                          "1. creating the milestone project schedule \n" +
                          "2. request milestone details\n" +
                          "3. Update\n");
        // Print the menu options for the user to select from

        string? ans = Console.ReadLine();// Read the user's input from the console
        int userChose;
        int.TryParse(ans, out userChose);// Convert the user's input to an integer
        switch (userChose)
        {
            case 0://exit
                return;
            case 1://creating the milestone project schedule
                s_bl.Milestone.CreatingTheMilestoneProjectSchedule();
                break;
            case 2:
                Console.WriteLine("enter id of milstone:");
                ans = Console.ReadLine();
                int id;
                int.TryParse(ans, out id);
                s_bl.Milestone.Read(id);
                break;
            case 3:
                Console.WriteLine("enter the id of the milstone to chane");
                ans = Console.ReadLine();
                int.TryParse(ans, out id);
                Milestone milestone = s_bl.Milestone.Read(id);
                Console.WriteLine("enter the alias to change:");
                string? alias = Console.ReadLine();
                bool susecful;
                Console.WriteLine("enter the Description to change");
                string? description = Console.ReadLine();
                Console.WriteLine("enter the forecast date to change:");
                ans = Console.ReadLine();
                DateTime forecastDate = DateTime.Now;
                susecful = DateTime.TryParse(ans, out forecastDate);
                if (!susecful)
                {
                    throw new ThePogramNotSuccedToConvert("the program does not succeed to convert"); // If the parsing fails, throw a ThePogramNotSuccedToConvert exception with the specified message
                }

                Console.WriteLine("enter the remarks to change:"); // Prompt the user to enter the new remarks
                string? remarks = Console.ReadLine(); // Read the user input and store it in the remarks variable

                Milestone milestone1 = new Milestone() // Create a new instance of the Milestone class and initialize its properties
                {
                    Id = milestone.Id, // Set the Id property to the value of milestone.Id
                    Alias = alias ?? milestone.Alias, // Set the Alias property to the value of alias if it is not null, otherwise use the original milestone.Alias
                    Description = description ?? milestone.Description, // Set the Description property to the value of description if it is
                    CreateDate = milestone.CreateDate, // Set the CreateDate property to the value of milestone.CreateDate
                    Status = milestone.Status, // Set the Status property to the value of milestone.Status
                    StartDate = milestone.StartDate, // Set the StartDate property to the value of milestone.StartDate
                    ForecastDate = milestone.ForecastDate, // Set the ForecastDate property to the value of milestone.ForecastDate
                    Deadline = milestone.Deadline, // Set the Deadline property to the value of milestone.Deadline
                    ActualEndDate = milestone.ActualEndDate, // Set the ActualEndDate property to the value of milestone.ActualEndDate
                    ProgressPercentage = milestone.ProgressPercentage, // Set the ProgressPercentage property to the value of milestone.ProgressPercentage
                    Remarks = remarks ?? milestone.Remarks, // Set the Remarks property to the value of remarks if it is not null, otherwise use the original milestone.Remarks
                    DependencyList = milestone.DependencyList, // Set the DependencyList property to the value of milestone.DependencyList
                };

                s_bl.Milestone.UpdateMilestone(milestone1); // Call the UpdateMilestone() method on the s_bl.Milestone object and pass in the new milestone object as a parameter to update the milestone with the changes
                break; // Exit the switch or case statement
            default:
                throw new ChoiseDoesNotExistException("This choice does not exist");
        }
    }
}

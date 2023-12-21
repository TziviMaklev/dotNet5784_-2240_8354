
using DalApi;
using DO;

namespace DalTest;
/// <summary>
/// initialize all of the lists with data
/// </summary>
public static class Initialization
{
    //the references to the lists of data
    //private static ITask? s_dalTask;
    //private static IEngineer? s_dalEngineer;
    //private static IDependency? s_dalDependency;
    private static IDal? s_dal;
    //use for making random numbers for the objects
    private static readonly Random s_rand = new();
    private const int MIN_ID = 100000000;
    private const int MAX_ID = 999999999;

    private static void CreateTask()
    {
        string[] _taskDescriptions =
        {
            "Write and test code for new features or bug fixes.",
            "Collaborate with team members to discuss requirements and solutions.",
            "Review and provide feedback on code written by other team members.",
            "Solving and debugging problems reported by users or identified through testing.",
            "Attend team meetings to discuss project progress and plan future work.",
            "Research new technologies and coding best practices to stay current.",
            "Write and maintain technical documentation for code and projects.",
            "Conduct code reviews and improve existing code to improve efficiency and maintainability.",
            "Collaboration with QA teams to ensure software quality and perform necessary tests.",
            "Work with project managers to estimate the time and effort required to complete tasks and meet project schedules.",
            "Performance Optimization: Identifying and improving code performance, analyzing bottlenecks and optimizing algorithms or code snippets to improve insurance.",
            "Database management: designing and maintaining databases, creating tables, writing complex queries and ensuring integrity and security.",
            "User interface (UI) design: collaboration with designers to create really visually appealing and user-friendly, implementing user interface elements and ensuring responsive design.",
            "Security testing: performing security assessments, vulnerability scanning and penetration testing to identify and address potential security risks in software applications.",
            "Version control management: using version control systems (such as Git) to manage and track code changes, communicate with team members and resolve merger conflicts.",
            "API integration: development and integration of application programming interfaces (API), the possibilities of communication and data exchange between different software systems.",
            "Continuous integration and deployment: Implementing CI/CD pipelines to automate the build, test and deployment processes, while ensuring fast and efficient software delivery.",
            "Machine learning and data analysis: Building and training machine learning models, analyzing large data sets and generating valuable insights for informed decision-making.",
            "Technical support: assisting users in solving technical problems, providing instructions regarding the use of the software and solving problems related to the software.",
            "Code Optimization and Recovery: Reviewing existing code bases, identifying areas for improvement, and modifying code to improve readability, maintainability, and performance"
        };
        string[] _taskAliases =
        {
            "Writing and testing code",
            "Collaboration and discussion of requirements",
            "Code review and feedback",
            "Troubleshooting and debugging",
            "Participation in team meetings and project planning",
            "Research and continuous learning",
            "Documentation of technical information",
            "Code review and modification",
            "Quality assurance and testing",
            "Task evaluation and project management",
            "Performance optimization",
            "Database management",
            "User interface (UI) design",
            "Security checks",
            "Version control management",
            "API integration",
            "Continuous integration and deployment",
            "For machine and data analysis",
            "Technical support",
            "Code Optimization and Recovery"
        };
        string[] _taskDeliverables =
        {
            "It is the code itself, which is a functional and reliable software solution.",
            "It is a clear understanding and agreement about the project requirements and its goals among the team members.",
            "The improved code quality and efficiency, as well as shared knowledge and expertise among the team.",
            "The identifying and fixing software problems, resulting in a more stable and functional application.",
            "The a well-coordinated team, effective project planning and effective task allocation.",
            "The acquisition of new knowledge, skills and insights that can be applied to improve software development methods.",
            "The comprehensive and accessible documentation that helps team members and stakeholders understand the software system.",
            "It is an optimized and maintainable code, leading to improved performance, readability and scalability.",
            "A reliable and high-quality software product that meets the detailed requirements and user expectations.",
            "The realistic project schedules, efficient resource allocation and successful completion of a project within specified constraints.",
            "Improving the performance and efficiency of the code or system, resulting in faster execution, reduced resource consumption, and improved user experience.",
            "A well-designed and efficiently structured database that stores and manages data efficiently, ensuring data integrity, security and easy retrieval.",
            "An intuitive and visually appealing user interface that enhances the user experience, promotes user engagement, and makes the software application easy to navigate and interact with.",
            "A secure software application with identified vulnerabilities and weaknesses, which reduces the risk of unauthorized access, data breaches and other security threats.",
            "A well-maintained and organized code base, with changes tracked, consolidated and documented effectively, which enables collaboration between team members and facilitates code stability.",
            "The successful integration of different software systems, allowing them to communicate and share data seamlessly, enabling improved functionality and interoperability.",
            "An automated and efficient software development process, enabling frequent and reliable releases of software, reducing manual errors and improving overall development efficiency.",
            "The development and deployment of accurate machine learning models, which can provide valuable insights and predictions from large data sets, enabling data-driven decision making.",
            "A quick and efficient solution of technical problems, providing assistance and guidance to users, ensuring smooth use of the software and maximizing user satisfaction.",
            "Optimized, clean, and maintainable code with improved performance, readability, and scalability, making it easier to maintain, modify, and improve the software application in the future."
        };
        EngineerExperience[] _taskEngineerExperiences =
        {
            EngineerExperience.AdvancedBeginner,
            EngineerExperience.Competent,
            EngineerExperience.Proficient,
            EngineerExperience.Proficient,
            EngineerExperience.Novice,
            EngineerExperience.Expert,
            EngineerExperience.Competent,
            EngineerExperience.Proficient,
            EngineerExperience.Proficient,
            EngineerExperience.Competent,
            EngineerExperience.Novice,
            EngineerExperience.Expert,
            EngineerExperience.Competent,
            EngineerExperience.Expert,
            EngineerExperience.AdvancedBeginner,
            EngineerExperience.Proficient,
            EngineerExperience.Competent,
            EngineerExperience.Proficient,
            EngineerExperience.Novice,
            EngineerExperience.AdvancedBeginner,
            EngineerExperience.Proficient
        };
        int[] _taskDays =
        {
            20,14,23,14,18,28,30,12,9,24,20,10,19,28,26,16,30,21,17,15
        };
        for (int i = 0; i < _taskAliases.Length; i++)
        {
            DateTime creationDate = DateTime.Now;
            DO.Task newTask = new DO.Task(0, _taskDescriptions[i], _taskAliases[i],
            false, _taskDeliverables[i], _taskEngineerExperiences[i], TimeSpan.FromDays(_taskDays[i]), creationDate,
            null, null, null, null, null, null, null);
            s_dal!.Task.Create(newTask);
        }
    }

    private static void CreateEngineer()
    {
        string[] _names =
        {
            "Benjamin",
            "David",
            "Alexander",
            "Samuel",
            "Daniel"
        };
        EngineerExperience[] _engineerExperiences =
        {
            EngineerExperience.Novice,
            EngineerExperience.AdvancedBeginner,
            EngineerExperience.Competent,
            EngineerExperience.Proficient,
            EngineerExperience.Expert
        };
        double[] _costs =
        {
            103.30,
            189.9,
            360.00,
            428.74,
            712.00
        };
        for (int i = 0; i < _names.Length; i++)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer!.Read(_id) != null);
            string _email = _names[i] + "@gmail.com";
            Engineer newEngineer = new Engineer(_id, _names[i], _email,
                _engineerExperiences[i], _costs[i]);
            s_dal!.Engineer.Create(newEngineer);
        }
    }

    private static void CreateDependency()
    {
        s_dal!.Dependency.Create(new Dependency(0, 2, 7));
        s_dal!.Dependency.Create(new Dependency(0, 2, 3));
        s_dal!.Dependency.Create(new Dependency(0, 3, 7));
        s_dal!.Dependency.Create(new Dependency(0, 4, 2));
        s_dal!.Dependency.Create(new Dependency(0, 5, 2));
        s_dal!.Dependency.Create(new Dependency(0, 5, 8));
        s_dal!.Dependency.Create(new Dependency(0, 6, 7));
        s_dal!.Dependency.Create(new Dependency(0, 8, 6));
        s_dal!.Dependency.Create(new Dependency(0, 9, 2));
        s_dal!.Dependency.Create(new Dependency(0, 9, 17));
        s_dal!.Dependency.Create(new Dependency(0, 10, 5));
        s_dal!.Dependency.Create(new Dependency(0, 10, 16));
        s_dal!.Dependency.Create(new Dependency(0, 10, 18));
        s_dal!.Dependency.Create(new Dependency(0, 11, 12));
        s_dal!.Dependency.Create(new Dependency(0, 12, 18));
        s_dal!.Dependency.Create(new Dependency(0, 12, 5));
        s_dal!.Dependency.Create(new Dependency(0, 13, 6));
        s_dal!.Dependency.Create(new Dependency(0, 13, 3));
        s_dal!.Dependency.Create(new Dependency(0, 14, 11));
        s_dal!.Dependency.Create(new Dependency(0, 14, 6));
        s_dal!.Dependency.Create(new Dependency(0, 15, 3));
        s_dal!.Dependency.Create(new Dependency(0, 15, 17));
        s_dal!.Dependency.Create(new Dependency(0, 15, 5));
        s_dal!.Dependency.Create(new Dependency(0, 16, 4));
        s_dal!.Dependency.Create(new Dependency(0, 16, 2));
        s_dal!.Dependency.Create(new Dependency(0, 17, 6));
        s_dal!.Dependency.Create(new Dependency(0, 17, 11));
        s_dal!.Dependency.Create(new Dependency(0, 18, 2));
        s_dal!.Dependency.Create(new Dependency(0, 18, 11));
        s_dal!.Dependency.Create(new Dependency(0, 18, 16));
        s_dal!.Dependency.Create(new Dependency(0, 19, 13));
        s_dal!.Dependency.Create(new Dependency(0, 19, 6));
        s_dal!.Dependency.Create(new Dependency(0, 19, 12));
        s_dal!.Dependency.Create(new Dependency(0, 20, 7));
        s_dal!.Dependency.Create(new Dependency(0, 20, 8));
        s_dal!.Dependency.Create(new Dependency(0, 20, 13));
        s_dal!.Dependency.Create(new Dependency(0, 21, 16));
        s_dal!.Dependency.Create(new Dependency(0, 21, 12));
        s_dal!.Dependency.Create(new Dependency(0, 21, 4));
        s_dal!.Dependency.Create(new Dependency(0, 21, 15));
    }

    /// <summary>
    /// make all the data in lists
    /// </summary>
    /// <param name="dalTask">task list</param>
    /// <param name="dalDependency">dependency list</param>
    /// <param name="dalEngineer">engineer list</param>
    /// <exception cref="NullReferenceException">there is no list like this</exception>
    /// 
    //public static void Do(IDal dal)
    public static void Do() //stage 4
    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dal = dal ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = DalApi.Factory.Get; //stage 4
        CreateTask();
        CreateDependency();
        CreateEngineer();
    }
}

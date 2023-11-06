using Dal;
using DalApi;
using DO;
using DalList;
using System.Data;

namespace DalTest;
using Dal;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;

    //use for making random numbers for the objects
    private static readonly Random s_rand = new();

    private static void createTask()
    {
        string[] taskDescriptions =
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
            "Work with project managers to estimate the time and effort required to complete tasks and meet project schedules."
        };
        string[] taskAliases =
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
            "Task evaluation and project management"
        };
        string[] taskDeliverables =
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
            "The realistic project schedules, efficient resource allocation and successful completion of a project within specified constraints."
        };
        EngineerExperience[] taskEngineerExperiences =
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
            EngineerExperience.Competent
        };
        for (int i = 0; i < taskAliases.Length; i++)
        {
            DateTime creationDate = new DateTime(2023, 11, 6);
            DO.Task newTask = new DO.Task(0, taskDescriptions[i], taskAliases[i],
            false, taskDeliverables[i], taskEngineerExperiences[i], creationDate,
            null, null, null, null, null, null, null);
            ITask? s_dalTask = null;
            s_dalTask.Create(newTask);
        }
    }

    public static void Do(ITask? s_dalTask, IDependency? s_dalDependency, IEngineer s_dalEngineer)
    {
        ITask? dalTask = null;
        IDependency? dalDependency = null;
        IEngineer? dalEngineer = null;
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        createTask();
        createDependency();
        createEngineer();
    }
}

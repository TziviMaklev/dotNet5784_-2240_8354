
namespace Dal;


internal static class DataSource
{
    internal static List<DO.Dependency?> Dependencies { get; } = new();
    internal static List<DO.Engineer?> Engineers { get; } = new();
    //internal static List<DO.EngineerExperience?> ?EngineerExperience { get; } = new();
    internal static List<DO.Task?> Tasks { get; } = new();

    /// <summary>
    /// the class for making running numbers
    /// </summary>
    internal static class Config
    {
        // TASK config 
        internal const int idTask = 2;
        private static int nextIdTask = idTask;
        public static int NextIdTask { get => nextIdTask++; }
        //dependency
        internal const int idDepency = 2;
        private static int nextIdDepency = idDepency;
        public static int NextIdDepency { get => nextIdDepency++; }

    }

}


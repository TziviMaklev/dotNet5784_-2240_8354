
namespace Dal;


internal static class DataSource
{
    internal static List<DO.Dependency?>Dependency { get; } = new();
    internal static List<DO.Engineer?> Engineer { get; } = new();
    //internal static List<DO.EngineerExperience?> ?EngineerExperience { get; } = new();
    internal static List<DO.Task?> Task { get; } = new();
    internal static class Config
    {
        // TASK config 
        internal const int idTask = 2;
        private static int nextIdTask = idTask;
        internal static int NextIdTask { get=> nextIdTask++;}
        //

    }

}


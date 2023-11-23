

namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    static string s_dependencies_xml = "dependencies";
    static string s_engineers_xml = "engineers";
    static string s_tasks_xml = "tasks";
    internal static int NextIdDepency { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdDepency"); }
    internal static int NextIdTask { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdTask"); }
}

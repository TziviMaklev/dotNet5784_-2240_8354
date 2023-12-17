

namespace Dal;
/// <summary>
/// a class for making runnung numbers
/// </summary>
internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextIdDepency { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdDepency"); }
    internal static int NextIdTask { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdTask"); }
}

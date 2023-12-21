
namespace BO;

public class Engineer
{
    internal int Id { get; init; }
    internal string? Name { get; init; }
    internal string? Email { get; set; }
    internal EngineerExperience Level { get; set; }
    internal double Cost {  get; set; }
}

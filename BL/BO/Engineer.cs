
namespace BO;

public class Engineer
{
    int Id { get; init; }
    string? Name { get; init; }
    string? Email { get; set; }
    EngineerExperience EngineerLevel { get; set; }
    float cost {  get; set; }
}

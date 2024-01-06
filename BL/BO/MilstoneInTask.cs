
namespace BO;

public class MilstoneInTask
{
    public int Id { get; init; }
    public string? Alias { get; init; }
    public override string ToString() => this.ToStringProperty();
}

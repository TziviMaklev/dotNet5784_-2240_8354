
namespace BlApi;
/// <summary>
/// 
/// </summary>

public interface IMilestone
{
    void CreatingTheMilestoneProjectSchedule() { }
    BO.Milestone RequestMilestoneDetails() { return new BO.Milestone(); }
    void UpdateMilestone() { }
}

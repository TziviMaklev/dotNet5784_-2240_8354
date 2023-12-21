
namespace BlApi;

public interface IMilestone
{
    public List<BO.Milestone> CreatingTheMilestoneProjectSchedule(List<DO.Dependency> recievedDeps);
    BO.Milestone RequestMilestoneDetails(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
}

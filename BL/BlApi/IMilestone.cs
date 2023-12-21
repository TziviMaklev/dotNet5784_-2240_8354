
namespace BlApi;

public interface IMilestone
{
    List<BO.Milestone> CreatingTheMilestoneProjectSchedule(List<DO.Dependency> recievedDeps);
    BO.Milestone RequestMilestoneDetails(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
}

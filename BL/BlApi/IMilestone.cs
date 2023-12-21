
namespace BlApi;

public interface IMilestone
{
    List<DO.Dependency> CreatingTheMilestoneProjectSchedule(List<DO.Dependency> recievedDeps) ;
    BO.Milestone RequestMilestoneDetails(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
}

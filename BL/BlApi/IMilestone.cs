
namespace BlApi;

public interface IMilestone
{
    void CreatingTheMilestoneProjectSchedule() ;
    BO.Milestone RequestMilestoneDetails(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
}

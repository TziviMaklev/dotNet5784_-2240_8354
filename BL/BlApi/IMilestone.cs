
namespace BlApi;

public interface IMilestone
{
    void CreatingTheMilestoneProjectSchedule();
    BO.Milestone Read(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
}

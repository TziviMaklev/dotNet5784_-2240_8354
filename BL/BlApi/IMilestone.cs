
namespace BlApi;

/// <summary>
/// interface for the milestone in the BL
/// </summary>
public interface IMilestone
{
    void CreatingTheMilestoneProjectSchedule();
    BO.Milestone Read(int id) ;
    void UpdateMilestone(BO.Milestone milestone) ;
    void SetDates(DateTime start, DateTime end) ;
}

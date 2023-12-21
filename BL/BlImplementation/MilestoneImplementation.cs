using BlApi;
using BO;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreatingTheMilestoneProjectSchedule()
    {
        //creat taskes

        
    }

    public Milestone RequestMilestoneDetails(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateMilestone(Milestone milestone)
    {
        throw new NotImplementedException();
    }
}

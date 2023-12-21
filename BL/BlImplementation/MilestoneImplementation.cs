using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using 

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public List<DO.Dependency> CreatingTheMilestoneProjectSchedule(List<DO.Dependency> recievedDeps)
    {
        List<DO.Dependency> newDependenceisList = new List<DO.Dependency>();

        var groupByTaskDependency = (from dependency in newDependenceisList
                                     where dependency?.DependentTask != null && dependency?.DependenceOnTask != null
                                     group dependency by dependency.DependentTask
                                     into dependencyListAfterGrouping
                                     let dependencyList = (from dependency in dependencyListAfterGrouping
                                                           select dependency.DependenceOnTask).Order()
                                     select new { _key = dependencyListAfterGrouping.Key, value = dependencyList }).ToList();
        var taskDependencyWhithoutDuplicate = (from dependency in groupByTaskDependency
                                               select dependency.value).Distinct().ToList();

        foreach (var milestone in taskDependencyWhithoutDuplicate)
        {
            int idNewMilestone = _dal.Task.Create(new DO.Task
            {
                Id = 111,
                Milestone = true,
                CreationDate = DateTime.Now
            });
            foreach (var dep in groupByTaskDependency)
            {
                if (dep.value == milestone!)
                {
                    newDependenceisList.Add(new DO.Dependency(dep._key, idNewMilestone, idNewMilestone - 1));
                }
            }



        }
        return newDependenceisList;

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

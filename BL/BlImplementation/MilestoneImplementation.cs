using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    /// <summary>
    /// save the key of the milestone and the list of the tasks that before it
    /// </summary>
    struct milestoneDepList
    {
        internal int milestoneNum;
        internal IEnumerable<int> depList;
    }
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreateProjectTimeline()
    {

    }
    internal List<BO.Milestone> creatingMilestones(List<DO.Dependency> recievedDeps)
    {
        List<DO.Dependency> newDependenceisList = new List<DO.Dependency>(recievedDeps);
        var milestones = new List<milestoneDepList>();//list of the milestones we made
        // A grouped list so that the key is a dependent task and the value is a list of previous tasks
        var groupByTaskDependency = (from dependency in newDependenceisList
                                     where dependency?.DependentTask != null && dependency?.DependenceOnTask != null
                                     group dependency by dependency.DependentTask
                                     into dependencyListAfterGrouping
                                     let dependencyList = (from dependency in dependencyListAfterGrouping
                                                           select dependency.DependenceOnTask).Order()
                                     select new { _key = dependencyListAfterGrouping.Key, value = dependencyList }).ToList();

        // Another filtered list of the list of values so that each value appears once
        var taskDependencyWhithoutDuplicate = (from dependency in groupByTaskDependency
                                               select dependency.value).Distinct().ToList();

        // Go through all the elements in the list taskDependencyWhithoutDuplicate and create a milestone for each value
        foreach (var dep in taskDependencyWhithoutDuplicate)
        {
            DO.Task milestone = new DO.Task
            {
                Id = 0,
                Alias= $"m{}"
                Milestone = true,
                CreationDate = DateTime.Now
            };
            //create a new milestone
            int idNewMilestone = _dal.Task.Create(milestone);
            milestones.Add(new milestoneDepList
            {
                milestoneNum = idNewMilestone,
                depList = dep
            });//add the milestone to themilestenes list
            //add the dependencies of the milestone to the new list
            foreach (var p in dep)
            {
                newDependenceisList.Add(new Dependency(0, idNewMilestone, p));
            }
        }
        //making for the all tasks that right after the milestones
        foreach (var milestonDep in groupByTaskDependency)
        {
            foreach (var m in milestones)//go over the milestones
            {
                if (milestonDep.value == m.depList)
                {
                    newDependenceisList.Add(new Dependency(0, milestonDep._key, m.milestoneNum));
                }
            }
        }
        //make the start milestone
        int idFirstMileston = _dal.Task.Create(new DO.Task
        {
            Id = 0,
            Milestone = true,
            CreationDate = DateTime.Now
        });
        //find the tasks that not dependnce on anything
        var independentTasks = (from t in _dal.Task.ReadAll()
                                join d in _dal.Dependency.ReadAll() on t.Id equals d.DependentTask into dependentTasks
                                where dependentTasks.Count() == 0
                                select t).ToList();
        foreach (var task in independentTasks)
        {
            newDependenceisList.Add(new Dependency(0, task.Id, idFirstMileston));
        }
        return new List<Milestone>();

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

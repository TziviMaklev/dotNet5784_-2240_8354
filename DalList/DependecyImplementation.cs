

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;

internal class DependecyImplementation : IDependency
{
    /// <summary>
    /// create a new item in the Dependency list
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>the id of the item we addad</returns>
    public int Create(Dependency item)
    {
        int id;
        var dependencyF = (from e in DataSource.Dependencies
                           where e.Id == item.Id
                           select e).ToList();
        if (dependencyF.Count == 0 && item.Id != 0)
            id = item.Id;
        else
            id = DataSource.Config.NextIdDepency;
        Dependency newDenendency = new Dependency(id, item.DependentTask, item.DependenceOnTask);
        DataSource.Dependencies.Add(item);
        return newDenendency.Id;
    }

    /// <summary>
    /// delete a item from the Dependency list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <exception cref="Exception">this id is not exist</exception>
    public void Delete(int id)

    {
        Dependency? foundDependency = DataSource.Dependencies.FirstOrDefault(d => d?.Id == id);
        if (foundDependency != null)
        {
            DataSource.Dependencies.Remove(foundDependency);
        }
        else
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// read an item from the Dependency list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <returns>return a reference to the item</returns>
    public Dependency? Read(int id)
    {
        Dependency? foundDependency = DataSource.Dependencies.FirstOrDefault(d => d?.Id == id);
        if (foundDependency != null)
        {
            return foundDependency;
        }
        return null;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(d => filter(d!));
    }

    /// <summary>
    /// read all of the Dependency list
    /// </summary>
    /// <returns>a list that copied from the Dependency list</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;

    }

    /// <summary>
    /// update one item in the Dependency list
    /// </summary>
    /// <param name="item">the item to update</param>
    public void Update(Dependency item)
    {
        Delete(item.Id);
        Create(item);
    }

    /// <summary>
    /// delete all items in the Dependency list
    /// </summary>
    public void Reset()
    {
        DataSource.Dependencies.Clear();
    }
}

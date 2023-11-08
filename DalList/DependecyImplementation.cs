

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependecyImplementation : IDependency
{
    /// <summary>
    /// create a new item in the Dependency list
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>the id of the item we addad</returns>
    /// <exception cref="Exception">the item with a same id of the parameter</exception>
    public int Create(Dependency item)
    {
        Dependency? foundDependency = DataSource.Dependencies.Find(x => (x.Id == item.Id));
        Console.WriteLine(DataSource.Dependencies.Find(x => x.Id == item.Id));
        if (foundDependency == null)
        {
            DataSource.Dependencies.Add(foundDependency);
        }
        else
        {
            throw new Exception($"Dependency with ID={item.Id} already exist");
        }
        return item.Id;
    }
    
    /// <summary>
    /// delete a item from the Dependency list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <exception cref="Exception">this id is not exist</exception>
    public void Delete(int id)

    {
        Dependency? foundDependency = DataSource.Dependencies.Find(x => x.Id == id);
        if (foundDependency != null)
        {
            DataSource.Dependencies.Remove(foundDependency);
        }
        else
        {
            throw new Exception($"Dependency with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// read an item from the Dependency list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <returns>return a reference to the item</returns>
    public Dependency? Read(int id)
    {
        Dependency? foundDependency = DataSource.Dependencies.Find(x => x.Id == id);
        if (foundDependency == null)
        {
            return foundDependency;
        }
        return null;
    }

    /// <summary>
    /// read all of the Dependency list
    /// </summary>
    /// <returns>a list that copied from the Dependency list</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
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
}

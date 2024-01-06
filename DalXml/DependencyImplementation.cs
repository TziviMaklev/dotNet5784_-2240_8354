
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;


internal class DependencyImplementation : IDependency
{





    /// <summary>
    /// create a new item in the Dependency list
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>the id of the item we addad</returns>
    public int Create(Dependency item)
    {
        List<Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = null;
        int id = Config.NextIdDepency;
        if (dependencies.Count > 0)
            dependency = dependencies.FirstOrDefault(d => item.Id == d.Id);
        if (dependency == null)
        {
            Dependency newDependency = item with { Id = id };
            dependencies?.Add(newDependency);
            XMLTools.SaveListToXMLSerializer(dependencies!, "dependencies");
        }
        else
        {
            throw new DalAlreadyExistsException("An dependency with this ID number already exists");
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
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = dependencies.FirstOrDefault(d => id == d.Id);
        if (dependency == null)
        {
            throw new DalAlreadyExistsException("An dependency with this ID number not exists");

        }
        else
        {

            dependencies?.Remove(dependency!);
            XMLTools.SaveListToXMLSerializer(dependencies!, "dependencies");
        }
    }

    /// <summary>
    /// read an item from the Dependency list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <returns>return a reference to the item</returns>
    public Dependency? Read(int id)
    {
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependencies!.FirstOrDefault(d => d.Id == id);
    }

    /// <summary>
    /// read items that making the "filter" true
    /// </summary>
    /// <param name="filter">the function that find the item</param>
    /// <returns>the item that making the function or null if it didn't found</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependencies!.FirstOrDefault(filter);
    }

    /// <summary>
    /// read all of the Dependency list
    /// </summary>
    /// <returns>a list that copied from the Dependency list</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null)
        {
            return XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies").Select(item => item);
        }
        else
        {
            return XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies").Where(filter);
        }
    }

    /// <summary>
    /// delete all items in the Dependency list
    /// </summary>
    public void Reset()
    {
        List<Dependency>? dependencies = new List<Dependency>();
        XMLTools.SaveListToXMLSerializer(dependencies, "dependencies");
    }

    /// <summary>
    /// update one item in the Dependency list
    /// </summary>
    /// <param name="item">the item to update</param>
    public void Update(Dependency item)
    {
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = dependencies.FirstOrDefault(d => item.Id == d.Id);
        if (dependency == null)
        {
            throw new DalAlreadyExistsException("An dependency with this ID number already exists");

        }
        else
        {
            dependencies.Remove(dependency);
            dependencies?.Add(item);
            XMLTools.SaveListToXMLSerializer(dependencies!, "dependencies");
        }

    }
}

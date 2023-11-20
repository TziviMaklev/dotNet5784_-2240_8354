

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// create a new item in the Engineer list
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>the id of the item we addad</returns>
    /// <exception cref="Exception">the item with a same id of the parameter</exception>
    public int Create(Engineer item)
    {
        var foundEngineer = DataSource.Engineers.FirstOrDefault(e=>e?.Id==item.Id);
        if (foundEngineer == null) {
             DataSource.Engineers.Add(item);
        }
        else
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exist");
        }
        return item.Id;
    }

    /// <summary>
    /// delete a item from the Engineer list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <exception cref="Exception">this id is not exist</exception>
    public void Delete(int id)
    {
        var foundEngineer = DataSource.Engineers.FirstOrDefault(e=>e?.Id==id);
        if (foundEngineer != null)
        {
            DataSource.Engineers.Remove(foundEngineer);
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// read an item from the Engineer list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <returns>return a reference to the item</returns>
    public Engineer? Read(int id)
    {
        Engineer? foundEngineer= DataSource.Engineers.FirstOrDefault(e=>e?.Id ==id);
        if (foundEngineer != null)
        {
            return foundEngineer;
        }
        return null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(e => filter(e!));
    }

    /// <summary>
    /// read all of the Dependency list
    /// </summary>
    /// <returns>a list that copied from the Dependency list</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
            if (filter != null)
            {
                return from item in DataSource.Engineers
                       where filter(item)
                       select item;
            }
            return from item in DataSource.Engineers
                   select item;
    }


    /// <summary>
    /// update one item in the Dependency list
    /// </summary>
    /// <param name="item">the item to update</param>
    public void Update(Engineer item)
    {
        Delete(item.Id);
        Create(item);
    }

    /// <summary>
    /// delete all items in the Engineer list
    /// </summary>
    public void Reset()
    {
        DataSource.Engineers.Clear();
    }
}

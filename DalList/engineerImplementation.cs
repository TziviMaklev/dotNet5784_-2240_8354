

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer? foundEngineer = DataSource.Engineers.Find(x => x.Id == item.Id);
        if (foundEngineer == null) {
            DataSource.Engineers.Add(foundEngineer);
        }
        else
        {
            throw new Exception("An Engineer object with such an ID already exists");
        }
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? foundEngineer = DataSource.Engineers.Find(x => x.Id == id);
        if (foundEngineer != null)
        {
            DataSource.Engineers.Remove(foundEngineer);
        }
        else
        {
            throw new Exception("An object of type Engineer with such an ID does not exist");
        }
    }

    public Engineer? Read(int id)
    {
        Engineer? foundEngineer = DataSource.Engineers.Find(x => x.Id == id);
        if (foundEngineer == null)
        {
            return foundEngineer;
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer> (DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Delete(item.Id);
        Create(item);
    }
}

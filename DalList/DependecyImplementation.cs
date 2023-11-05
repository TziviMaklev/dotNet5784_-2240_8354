

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependecyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        Dependency? foundEngineer = DataSource.Dependencies.Find(x => x.Id == item.Id);
        if (foundEngineer == null)
        {
            DataSource.Dependencies.Add(foundEngineer);
        }
        else
        {
            throw new Exception($"Dependency with ID={item.Id} already exist");
        }
        return item.Id;
    }
    

    public void Delete(int id)
    {
        Dependency? foundEngineer = DataSource.Dependencies.Find(x => x.Id == id);
        if (foundEngineer != null)
        {
            DataSource.Dependencies.Remove(foundEngineer);
        }
        else
        {
            throw new Exception($"Dependency with ID={id} does Not exist");
        }
    }

    public Dependency? Read(int id)
    {
        Dependency? foundEngineer = DataSource.Dependencies.Find(x => x.Id == id);
        if (foundEngineer == null)
        {
            return foundEngineer;
        }
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Delete(item.Id);
        Create(item);
    }
}

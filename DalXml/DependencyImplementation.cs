
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        List <Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = dependencies.FirstOrDefault( d => item.Id == d.Id );
        if (dependency == null)
        {
            Dependency newDependency = item;
            dependencies?.Add( newDependency );
            XMLTools.SaveListToXMLSerializer(dependencies!, "dependencies");
        }
        else
        {
            throw new DalAlreadyExistsException("An dependency with this ID number already exists");
        }
        return item.Id;
    }

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

    public Dependency? Read(int id)
    {
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependencies!.FirstOrDefault(d => d.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependencies!.FirstOrDefault(filter);
    }

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

    public void Reset()
    {
        List<Dependency>? dependencies = new List<Dependency> ();
        XMLTools.SaveListToXMLSerializer(dependencies, "dependencies");
    }

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

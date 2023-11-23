

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
        List <Dependency>? Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("Dependencies");
        Dependency? dependency = Dependencies.FirstOrDefault( d => item.Id == d.Id );
        if (dependency == null)
        {
            Dependency newDependency = item;
            Dependencies?.Add( newDependency );
            XMLTools.SaveListToXMLSerializer(Dependencies!, "Dependencies");
        }
        else
        {
            throw new DalAlreadyExistsException("An dependency with this ID number already exists");
        }
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Dependency>? Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = Dependencies.FirstOrDefault(d => id == d.Id);
        if (dependency == null)
        {
            throw new DalAlreadyExistsException("An dependency with this ID number not exists");

        }
        else
        {

            Dependencies?.Add(dependency);
            XMLTools.SaveListToXMLSerializer(Dependencies!, "dependencies");
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
        
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}

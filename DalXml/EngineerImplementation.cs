

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    XDocument? doc;
    public int Create(Engineer item)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.Id == item.Id);
        if (engineer != null)
            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        Engineer new_engineer = item;
        engineers?.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        return new_engineer.Id;
    }

    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers.FirstOrDefault(d => id == d.Id);
        if (engineer == null)
        {
            throw new DalAlreadyExistsException("An engineer with this ID number not exists");

        }
        else
        {

            engineers?.Add(engineer);
            XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        }
    }

    public Engineer? Read(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault( e => e.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter == null)
        {
            return XMLTools.LoadListFromXMLSerializer<Engineer>("engineers").Select(item => item);
        }
        else
        {
            return XMLTools.LoadListFromXMLSerializer<Engineer>("engineers").Where(filter);
        }

    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}

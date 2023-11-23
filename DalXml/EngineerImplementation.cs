

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

        XElement engineers = XMLTools.LoadListFromXMLElement("enginers");
        var engineer = engineers.Descendants("Engineer")
            .FirstOrDefault(e => e.Attribute("Id")!.Value.Equals(item.Id));
        if (engineer != null)
        {
            engineers.Add(item);
            XMLTools.SaveListToXMLElement(engineers, "engineers");
            return item.Id;
        }
        else
        {
            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        }
        //List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        //Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.Id == item.Id);
        //if (engineer != null)
        //    throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        //Engineer new_engineer = item;
        //engineers?.Add(new_engineer);
        //XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        //return new_engineer.Id;
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
        //doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");
        //List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
        {
            XElement engineers = XMLTools.LoadListFromXMLElement("engineers");
            var allEngineers = engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Attribute("Id")!.Value),
                                   Name = e.Attribute("Name")!.Value,
                                   Email = e.Attribute("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Attribute("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Attribute("Cost")!.Value)
                               });
            return allEngineers;
            //return XMLTools.LoadListFromXMLSerializer<Engineer>("engineers").Select(item => item);
        }
        else
        {
            XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
            IEnumerable<Engineer?> foundEngineer = engineers.Elements("Engineer")
                .Select(e=>new Engineer
                {
                    Id = Convert.ToInt32(e.Attribute("Id")!.Value),
                    Name = e.Attribute("Name")!.Value,
                    Email = e.Attribute("Email")!.Value,
                    Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Attribute("Level")!.Value),
                    Cost = Convert.ToDouble(e.Attribute("Cost")!.Value)
                })
                .Where(filter);
            return foundEngineer;
            //return XMLTools.LoadListFromXMLSerializer<Engineer>("engineers").Where(filter);
        }

    }

    public void Reset()
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        //לא גמור!!!!!!
    }

    public void Update(Engineer item)
    {
        //צריך לשות!!!!!!!!
        throw new NotImplementedException();
    }
}

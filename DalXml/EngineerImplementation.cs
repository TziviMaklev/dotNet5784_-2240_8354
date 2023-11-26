

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    XDocument? doc;
    public int Create(Engineer item)
    {

        XElement engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = engineers.Descendants("engineer")
            .FirstOrDefault(e => e.Attribute("Id")!.Value.Equals(item.Id));
        XElement? returnEngineer = new XElement("Engineer",
                                                 new XAttribute("Id", item.Id),
                                                 new XAttribute("Name", item.Name),
                                                 new XAttribute("Email", item.Email),
                                                 new XAttribute("Level", item.Level),
                                                 new XAttribute("Cost", item.Cost));
        if (engineer == null)
        {
            engineers.Add(returnEngineer);
            XMLTools.SaveListToXMLElement(engineers, "Engineers");
            return item.Id;
        }
        else
        {
            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        }
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

            engineers?.Remove(engineer);
            XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        }
    }

    public Engineer? Read(int id)
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = (engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Attribute("Id")!.Value),
                                   Name = e.Attribute("Name")!.Value,
                                   Email = e.Attribute("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Attribute("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Attribute("Cost")!.Value)
                               })).FirstOrDefault(e => e.Id == id);
        return engineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {

        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = (engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Attribute("Id")!.Value),
                                   Name = e.Attribute("Name")!.Value,
                                   Email = e.Attribute("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Attribute("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Attribute("Cost")!.Value)
                               })).FirstOrDefault(filter);
        return engineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
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
        }
        else
        {
            XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
            IEnumerable<Engineer?> foundEngineer = engineers.Elements("Engineer")
                .Select(e => new Engineer
                {
                    Id = Convert.ToInt32(e.Attribute("Id")!.Value),
                    Name = e.Attribute("Name")!.Value,
                    Email = e.Attribute("Email")!.Value,
                    Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Attribute("Level")!.Value),
                    Cost = Convert.ToDouble(e.Attribute("Cost")!.Value)
                })
                .Where(filter);
            return foundEngineer;
        }

    }

    public void Reset()
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        engineers.Elements().Remove();
        XMLTools.SaveListToXMLElement(engineers, "engineers");
    }

    public void Update(Engineer item)
    {
        Delete(item.Id);
        Create(item);
    }
}
//namespace Dal;
//using DalApi;
//using DO;
//using System;
//using System.Collections.Generic;
//using System.Reflection.PortableExecutable;
//using System.Reflection;
//using System.Text.Json;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using System.Threading.Tasks;

//internal class EngineerImplementation : IEngineer
//{
//    const string FILENAME = @"..\xml\engineers.xml";


//    /// <summary>
//    /// create a new engineer entity
//    /// </summary>
//    /// <param name="item">wanted engineer to add</param>
//    /// <returns>new entity</returns>
//    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
//    public int Create(Engineer item)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.Id == item.Id);
//        if (engineer != null)
//            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
//        Engineer new_engineer = item with { };
//        engineers?.Add(new_engineer);
//        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
//        return new_engineer.Id;
//    }

//    /// <summary>
//    /// delete engineer entity
//    /// </summary>
//    /// <param name="id">wanted engineer to delete</param>
//    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
//    public void Delete(int id)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.Id == id);
//        if (engineer is null)
//            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
//        engineers?.Remove(engineer);
//        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
//    }


//    /// <summary>
//    /// returns a Engineer by some kind attribute.
//    /// </summary>
//    /// <param name="filter">The attributethat the search works by</param>
//    public Engineer? Read(Func<Engineer, bool> filter)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        return engineers!.FirstOrDefault(filter);
//    }

//    /// <summary>
//    /// display one engineer
//    /// </summary>
//    /// <param name="id">the wanted engineer</param>
//    /// <returns></returns>
//    public Engineer? Read(int id)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        return (engineers!.Find(element => element!.Id == id));
//    }

//    /// <summary>
//    /// return all the engineer's entities
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        if (filter == null)
//            return engineers!.Select(item => item);
//        else
//            return engineers!.Where(filter);
//    }

//    /// <summary>
//    /// update specific engineer entity
//    /// </summary>
//    /// <param name="item">wanted engineer's</param>
//    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
//    public void Update(Engineer item)
//    {
//        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.Id == item.Id);
//        if (engineer == null)
//            throw new DalDoesNotExistException($"engineer with ID={item.Id} already not exists\n");
//        else
//        {
//            engineers?.Remove(engineer);
//            engineers?.Add(item);
//            XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
//        }

//    }
//    public void Reset()
//    {
//        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
//        engineers.Elements().Remove();
//        XMLTools.SaveListToXMLElement(engineers, "engineers");
//    }
//}

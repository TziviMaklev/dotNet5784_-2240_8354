

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
    /// <summary>
    /// create a new item in the Engineer list
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>the id of the item we addad</returns>
    /// <exception cref="Exception">the item with a same id of the parameter</exception>
    public int Create(Engineer item)
    {

        XElement engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = engineers.Descendants("Engineer")
            .FirstOrDefault(e => e.Element("Id")!.Value.Equals(item.Id));
        XElement? returnEngineer = new XElement("Engineer",
                                                 new XElement("Id", item.Id),
                                                 new XElement("Name", item.Name),
                                                 new XElement("Email", item.Email),
                                                 new XElement("Level", item.Level),
                                                 new XElement("Cost", item.Cost));
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

    /// <summary>
    /// delete a item from the Engineer list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <exception cref="Exception">this id is not exist</exception>
    public void Delete(int id)
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = engineers.Descendants("Engineer")
            .FirstOrDefault(e => Convert.ToInt32(e.Element("Id")!.Value).Equals(id));
        if (engineer == null)
        {
            throw new DalAlreadyExistsException("An engineer with this ID number not exists");

        }
        else
        {

            engineer.Remove();
            XMLTools.SaveListToXMLElement(engineers!, "engineers");
        }
    }

    /// <summary>
    /// read an item from the Engineer list
    /// </summary>
    /// <param name="id">the id of the item to delete</param>
    /// <returns>return a reference to the item</returns>
    public Engineer? Read(int id)
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = (engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Element("Id")!.Value),
                                   Name = e.Element("Name")!.Value,
                                   Email = e.Element("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Element("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Element("Cost")!.Value)
                               })).FirstOrDefault(e => e.Id == id);
        return engineer;
    }

    /// <summary>
    /// read an item from the Engineer list according to the filter function
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>return a reference to the item</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {

        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        var engineer = (engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Element("Id")!.Value),
                                   Name = e.Element("Name")!.Value,
                                   Email = e.Element("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Element("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Element("Cost")!.Value)
                               })).FirstOrDefault(filter);
        return engineer;
    }

    /// <summary>
    /// read all of the Dependency list
    /// </summary>
    /// <returns>a list that copied from the Dependency list</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter == null)
        {
            XElement engineers = XMLTools.LoadListFromXMLElement("engineers");
            var allEngineers = engineers.Elements("Engineer")
                               .Select(e => new Engineer
                               {
                                   Id = Convert.ToInt32(e.Element("Id")!.Value),
                                   Name = e.Element("Name")!.Value,
                                   Email = e.Element("Email")!.Value,
                                   Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Element("Level")!.Value),
                                   Cost = Convert.ToDouble(e.Element("Cost")!.Value)
                               });
            return allEngineers;
        }
        else
        {
            XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
            IEnumerable<Engineer?> foundEngineer = engineers.Elements("Engineer")
                .Select(e => new Engineer
                {
                    Id = Convert.ToInt32(e.Element("Id")!.Value),
                    Name = e.Element("Name")!.Value,
                    Email = e.Element("Email")!.Value,
                    Level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), e.Element("Level")!.Value),
                    Cost = Convert.ToDouble(e.Element("Cost")!.Value)
                })
                .Where(filter);
            return foundEngineer;
        }

    }

    /// <summary>
    /// delete all items in the Engineer list
    /// </summary>
    public void Reset()
    {
        XElement? engineers = XMLTools.LoadListFromXMLElement("engineers");
        engineers.Elements().Remove();
        XMLTools.SaveListToXMLElement(engineers, "engineers");
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
}


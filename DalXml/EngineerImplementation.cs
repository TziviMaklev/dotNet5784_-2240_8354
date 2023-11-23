

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    XDocument? doc;
    public int Create(Engineer item)
    {
        doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");
        try
        {
            var isIdExist = doc.Descendants("Engineer")
                            .FirstOrDefault(el => el.Attribute("Id")!.Value.Equals(item.Id));
            if (isIdExist == null)
            {
                doc.Element("ArrayOfEngineer")!.Add(new XElement("Engineer",
                    new XAttribute("Id", item.Id),
                    new XAttribute("Name", item.Name),
                    new XAttribute("Email", item.Email),
                    new XAttribute("Level", item.Level),
                    new XAttribute("Cost", item.Cost)));
            }
            else
            {
                throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exist");
            }
        }
        catch{
            throw ;
        }
        return item.Id;
    }

    public void Delete(int id)
    {
        doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");

        var isIdExist = doc.Descendants("Engineer")
                           .FirstOrDefault(el => el.Attribute("Id")!.Value.Equals(id));
        if(isIdExist != null)
        {
            doc.Elements("Engineer")
                !.FirstOrDefault(el => el.Attribute("Id")!.Value.Equals(id))
                !.Remove();
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        }
    }

    public Engineer? Read(int id)
    {
        doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");
        var isIdExist = doc.Elements("Engineer")
            .First(el => el.Attribute("Id")!.Value.Equals(id));
        if(isIdExist != null)
        {
            var item = (from e in doc.Elements("Engineer")
                        where Convert.ToInt32(e.Attribute("Id")!.Value).Equals(id)
                        select e).First();
            Engineer engineer = new Engineer(Convert.ToInt32(item.Attribute("Id")!.Value),
                item.Attribute("Name")!.Value,
                item.Attribute("Email")!.Value,
                (EngineerExperience)Enum.Parse(typeof(EngineerExperience), item.Attribute("Level")!.Value),
                Convert.ToDouble(item.Attribute("Cost")!.Value));
            return engineer;
        }
        return null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers?.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        doc ??= XDocument.Load("C:/Users/PC/Desktop/mini project C#/dotNet5784_-2240_8354/xml/engineers.xml");
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

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

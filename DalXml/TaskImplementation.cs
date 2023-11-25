
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        List<Task>? tasksL = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int id = Config.NextIdTask;
        Task? task = null;
        if (tasksL.Count > 0)
        {
            task = tasksL.FirstOrDefault(d => id == d.Id);
        }
        if (task == null)
        {
            Task newTask = item with { Id = id};
            tasksL.Add(newTask);
            XMLTools.SaveListToXMLSerializer(tasksL!, "tasks");
        }
        else
        {
            throw new DalAlreadyExistsException("An task with this ID number already exists");
        }
        return item.Id;


    }

    public void Delete(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasks.FirstOrDefault(d => id == d.Id);
        if (task == null)
        {
            throw new DalAlreadyExistsException("An task with this ID number not exists");

        }
        else
        {

            tasks?.Remove(task);
            XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
        }

    }

    public Task? Read(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(t => t.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter == null)
        {
            return XMLTools.LoadListFromXMLSerializer<Task>("tasks").Select(item => item);
        }
        else
        {
            return XMLTools.LoadListFromXMLSerializer<Task>("tasks").Where(filter);
        }
    }

    public void Reset()
    {
        List<Task>? tasks = new List<Task>();
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
    }

    public void Update(Task item)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasks.FirstOrDefault(d => item.Id == d.Id);
        if (task == null)
        {
            throw new DalAlreadyExistsException("An task with this ID number already exists");

        }
        else
        {
            tasks.Remove(task);
            tasks?.Add(item);
            XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
        }
    }
}
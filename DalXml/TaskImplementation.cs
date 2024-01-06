
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// creates a new task in the list of tasks
    /// </summary>
    /// <param name="item">the item to add</param>
    /// <returns>returns the id of the task that created</returns>
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
        return id;


    }

    /// <summary>
    /// delete a task from the tasks list
    /// </summary>
    /// <param name="id">the id of the task to delete</param>
    /// <exception cref="Exception">the object is not exist</exception>
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

    /// <summary>
    /// read a task from the task list
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns>return a reference to the task</returns>
    public Task? Read(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// read items that making the "filter" true
    /// </summary>
    /// <param name="filter">the function that find the item</param>
    /// <returns>the item that making the function or null if it didn't found</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(filter);
    }

    /// <summary>
    /// read all list of tasks
    /// </summary>
    /// <returns>return a new list that the same as the one exist</returns>
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

    /// <summary>
    /// delete all items in the Task list
    /// </summary>
    public void Reset()
    {
        List<Task>? tasks = new List<Task>();
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
    }

    /// <summary>
    /// delete one task from the list and add one with the same id
    /// </summary>
    /// <param name="item">the new item to update</param>
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

namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using ITask = BlApi.ITask;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEnumerable<BO.TaskInList>? RequestTaskList(Func<BO.TaskInList, bool>? filter = null)
    {
        // Retrieve all tasks from the data access layer and create a new list of BO.TaskInList objects
        var taskList = (from DO.Task doTask in _dal.Task.ReadAll()
                        select new BO.TaskInList
                        {
                            Id = doTask.Id,
                            Alias = doTask.Alias,
                            Description = doTask.Description,
                            Status = (BO.Status)(doTask.ScheduledDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.CompletionDate is null ? 2
                                               : 3)
                        });
        // Check if a filter is provided
        if (filter != null)
        {
            // Apply the filter to the task list
            var taskListWhithFilter = (from item in taskList
                                       where filter(item)
                                       select item);
            // Return the filtered task list
            return taskListWhithFilter;
        }
        else
        {
            // Return the original task list
            return taskList;
        }
    }

    /// <summary>
    /// Retrieve a task object based on the provided ID
    /// </summary>
    /// <param name="id">The ID of the task to retrieve</param>
    /// <returns>The task object with the provided ID</returns>
    public BO.Task GetTask(int id)
    {
        try
        {
            DO.Task? task = _dal.Task.Read(id); // Read the task with the given ID from the data layer
            return task == null// If the task is null
                ? throw new DO.DalDoesNotExistException($"Task with ID={id} does Not exist")//throw a DalDoesNotExistException with a custom error message
                : new BO.Task()// Otherwise, create a new BO.Task object
                {
                    Id = task!.Id, // Set the Id property of the BO.Task object to the value of task.Id
                    Alias = task!.Alias, // Set the Alias property of the BO.Task object to the value of task.Alias
                    Description = task!.Description, // Set the Description property of the BO.Task object to the value of task.Description
                    CreatedAtDate = task!.CreationDate, // Set the CreatedAtDate property of the BO.Task object to the value of task.CreationDate
                    Status = (BO.Status)( // Set the Status property of the BO.Task object based on the task's scheduling and completion status
                                                task.ScheduledDate is null ? 0
                                               : task.StartDate is null ? 1
                                               : task.CompletionDate is null ? 2
                                               : 3),
                Dependencies = (from DO.Dependency doDependency in _dal.Dependency!.ReadAll(d => d.DependentTask == task!.Id)
                                select new BO.TaskInList()
                                {
                                    Id = doDependency.DependenceOnTask,// Set the Id property of the BO.TaskInList object to the DependenceOnTask value of the dependency
                                    Alias = _dal.Task!.Read(doDependency.DependenceOnTask)?.Alias,// Set the Alias property of the BO.TaskInList object to the Alias property of the task retrieved from the data layer
                                    Description = _dal.Task!.Read(doDependency.DependenceOnTask)?.Description,// Set the Description property of the BO.TaskInList object to the Description property of the task retrieved from the data layer
                                    Status = (BO.Status)(
                                    _dal.Task!.Read(doDependency.DependenceOnTask)?.ScheduledDate is null ? 0 // Set the Status property of the BO.TaskInList object based on the task's scheduling and completion status, similar to the previous code snippet
                                    : _dal.Task!.Read(doDependency.DependenceOnTask)?.StartDate is null ? 1
                                    : _dal.Task!.Read(doDependency.DependenceOnTask)?.CompletionDate is null ? 2
                                    : 3)
                                }).ToList(),
                Milstone = new BO.MilstoneInTask()
                {
                    // Set the Id property of the BO.MilstoneInTask object to the Id of the dependent task
                    Id = _dal.Task!.Read(_dal.Dependency!.Read(dep =>
                    {
                        // Retrieve the dependent task that is also a milestone
                        _dal.Task!.Read(task => task.Milestone && task.Id == dep.DependentTask);
                        return dep.DependenceOnTask == task.Id;
                    })!.DependentTask)!.Id,
                    // Set the Alias property of the BO.MilstoneInTask object to the Alias of the dependent task
                    Alias = _dal.Task!.Read(_dal.Dependency!.Read(dep =>
                    {
                        // Retrieve the dependent task that is also a milestone
                        _dal.Task!.Read(task => task.Milestone && task.Id == dep.DependentTask);
                        return dep.DependenceOnTask == task.Id;
                    })!.DependentTask)?.Alias
                },
                RequiredEffortTime = task.RequiredEffortTime,// Set the RequiredEffortTime property of the task
                ScheduledDate = task.ScheduledDate,// Set the ScheduledDate property of the task
                StartDate = task.StartDate,// Set the ScheduledDate property of the task
                ForecastDate = task.StartDate+task.RequiredEffortTime,// Set the ForecastDate property of the task by adding the StartDate and RequiredEffortTime
                DeadlineDate = task.DeadlineDate,// Set the DeadlineDate property of the task
                CompleteDate = task.CompletionDate,// Set the CompleteDate property of the task
                Deliverables = task.Deliverables,// Set the Deliverables property of the task
                Remarks = task.Remarks,// Set the Remarks property of the task
                ComplexityTask = (BO.EngineerExperience)task.ComplexityTask!,// Set the ComplexityTask property of the task, casting it to the BO.EngineerExperience type
                Engineer = new EngineerInTask()
                {
                    // Set the Id property of the EngineerInTask object to the EngineerId of the task
                    Id = (int)task.EngineerId!,
                    // Set the Name property of the EngineerInTask object to the Name of the engineer retrieved from the data layer
                    Name = _dal.Engineer.Read(id)!.Name
                }
            };

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }
    
    /// <summary>
    /// Adds a new task to the data layer.
    /// </summary>
    /// <param name="task">The task object to be added.</param>
    /// <returns>The ID of the newly created task.</returns> 
    public int AddTask(BO.Task task)
    {
        DO.Task doTask = new DO.Task(
            task.Id, // Set the Id property of the DO.Task object to the value of task.Id
            task.Description!, // Set the Description property of the DO.Task object to the value of task.Description
            task.Alias!, // Set the Alias property of the DO.Task object to the value of task.Alias
            false, // Set the IsDeleted property of the DO.Task object to false
            task.Deliverables!, // Set the Deliverables property of the DO.Task object to the value of task.Deliverables
            (DO.EngineerExperience)task.ComplexityTask, // Cast the ComplexityTask property of the BO.Task object to DO.EngineerExperience enum and assign it to the ComplexityTask property of the DO.Task object
            task.RequiredEffortTime, // Set the RequiredEffortTime property of the DO.Task object to the value of task.RequiredEffortTime
            task.CreatedAtDate, // Set the CreatedAtDate property of the DO.Task object to the value of task.CreatedAtDate
            task.StartDate, // Set the StartDate property of the DO.Task object to the value of task.StartDate
            task.ScheduledDate, // Set the ScheduledDate property of the DO.Task object to the value of task.ScheduledDate
            task.DeadlineDate, // Set the DeadlineDate property of the DO.Task object to the value of task.DeadlineDate
            task.CompleteDate, // Set the CompleteDate property of the DO.Task object to the value of task.CompleteDate
            task.Remarks, // Set the Remarks property of the DO.Task object to the value of task.Remarks
            task.Engineer?.Id); // Set the EngineerId property of the DO.Task object to the Id property of the task.Engineer object if it is not null, otherwise set it to null
        try
        {
            int idNewTask = _dal.Task.Create(doTask); 
            if (task.Dependencies != null)
            {
                foreach (var dep in task.Dependencies)
                {
                    _dal.Dependency.Create(new DO.Dependency(0, idNewTask, dep.Id));
                }
            }
            return idNewTask;
        }
        catch (DO.DalAlreadyExistsException ex) // Catch the DalAlreadyExistsException
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex)
    ;
        }
    }
    
    /// <summary>
    /// Remove a task with the provided ID
    /// </summary>
    /// <param name="id">The ID of the task to remove</param>
    public void RemoveTask(int id)
    {
        // Read the task with the given ID from the data access layer
        DO.Task? task = _dal.Task.Read(id);

        // Check if the task object is not null
        if (task != null)
        {
            // Delete the task from the data access layer
            _dal.Task.Delete(id);
        }
        else
        {
            // Throw an exception indicating that the task with the provided ID does not exist
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Update a task with the provided task object
    /// </summary>
    /// <param name="task">The task object to update</param>
    public void UpdateTask(BO.Task task)
    {

        // Create a new instance of the DO.Task class using the provided properties from the task object
        DO.Task? doTask = new DO.Task(
            task.Id,
            task.Description!,
            task.Alias!,
            false,
            task.Deliverables!,
            (DO.EngineerExperience)task.ComplexityTask,
            task.RequiredEffortTime,
            task.CreatedAtDate,
            task.StartDate,
            task.ScheduledDate,
            task.DeadlineDate,
            task.CompleteDate,
            task.Remarks,
            task.Engineer?.Id);

        // Check if the doTask object is not null
        if (doTask != null)
        {
            // Update the task in the data access layer
            _dal.Task.Update(doTask);
        }
        else
        {
            // Throw an exception indicating that the engineer with the provided ID does not exist
            throw new BO.BlDoesNotExistException($"engineer with ID={task.Id} does Not exist");
        }
    }

}

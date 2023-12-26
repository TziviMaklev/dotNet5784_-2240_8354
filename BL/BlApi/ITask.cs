
using System.Collections.Generic;

namespace BlApi;

/// <summary>
/// interface for the task in the BL
/// </summary>
public interface ITask
{
    IEnumerable<BO.TaskInList>? RequestTaskList(Func<BO.TaskInList, bool>? filter = null) ;
    BO.Task GetTask(int id);
    int AddTask(BO.Task task);
    void RemoveTask(int id);
    void UpdateTask(BO.Task task);
}

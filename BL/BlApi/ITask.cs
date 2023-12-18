
using System.Collections.Generic;

namespace BlApi;

public interface ITask
{
    IEnumerable<BO.Task>? RequestTaskList(Func<BO.Task, bool> filter) ;
    BO.Task GetTask(int id);
    void AddTask(BO.Task task);
    void RemoveTask(int id);
    void UpdateTask(BO.Task task);
}

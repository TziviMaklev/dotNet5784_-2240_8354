
using System.Collections.Generic;

namespace BlApi;

public interface ITask
{
    IEnumerable<BO.TaskInList>? RequestTaskList(Func<BO.TaskInList, bool> filter) ;
    BO.Task GetTask(int id);
    int AddTask(BO.Task task);
    void RemoveTask(int id);
    void UpdateTask(BO.Task task);
}

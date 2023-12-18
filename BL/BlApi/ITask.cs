
using System.Collections.Generic;

namespace BlApi;

public interface ITask
{
    List<BO.Task> RequestTaskList() {  return new List<BO.Task>(); }
    BO.Task GetTask(int id) {  return new BO.Task(); }
    void AddTask(BO.Task task) { }
    void RemoveTask(BO.Task task) {  }
    void UpdateTask(BO.Task task) { }
}

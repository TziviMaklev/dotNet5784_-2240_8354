
using System.Collections.Generic;

namespace BlApi;

public interface IEngineer
{
    List<BO.Engineer> RequestEngineersList() {  return new List<BO.Engineer>(); }
    BO.Engineer RequestEngineerDetails() {  return new BO.Engineer(); } 
    void AddEngineer() { }
    void RemoveEngineer() { }
    void UpdateEngineerDetails() { }
}

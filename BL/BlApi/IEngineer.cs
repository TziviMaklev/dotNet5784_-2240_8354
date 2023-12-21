


using System.Collections.Generic;

namespace BlApi;

public interface IEngineer
{
    IEnumerable<BO.Engineer>? RequestEngineersList(Func<BO.Engineer, bool> ?filter) ;
    BO.Engineer RequestEngineerDetails(int id) ; 
    int AddEngineer(BO.Engineer engineer) ;
    void RemoveEngineer(int id);
    void UpdateEngineerDetails(BO.Engineer engineer);
}

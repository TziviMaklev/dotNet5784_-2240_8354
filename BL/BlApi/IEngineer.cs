


using System.Collections.Generic;

namespace BlApi;
/// <summary>
/// interface for the engineer in the BL
/// </summary>
public interface IEngineer
{
    IEnumerable<BO.Engineer>? RequestEngineersList(Func<BO.Engineer, bool> ?filter = null) ;
    BO.Engineer RequestEngineerDetails(int id) ; 
    int AddEngineer(BO.Engineer engineer) ;
    void RemoveEngineer(int id);
    void UpdateEngineerDetails(BO.Engineer engineer);
}

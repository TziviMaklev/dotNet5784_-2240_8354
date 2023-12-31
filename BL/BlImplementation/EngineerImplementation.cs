
namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// add engineer to the DB
    /// </summary>
    /// <param name="engineer">engineer to add</param>
    /// <returns>id of the engineer that added</returns>
    /// <exception cref="BO.BlAlreadyExistsException">this id is already exist</exception>
    public int AddEngineer(BO.Engineer engineer)
    {
        DO.Engineer doEngineer = new DO.Engineer() 
        { 
          Id = engineer.Id,
          Name = engineer.Name!,
          Email = engineer.Email!,
          Level = (DO.EngineerExperience)engineer.Level,
          Cost = engineer.Cost,
        };
        try
        {
            int idNewEngineer = _dal.Engineer.Create(doEngineer);
            return idNewEngineer;
        }
        catch (DO.DalAlreadyExistsException ex) {
            throw  new BO.BlAlreadyExistsException($"Engineer with ID={engineer.Id} already exists", ex);
        };
    }
    /// <summary>
    /// delete an engineer
    /// </summary>
    /// <param name="id">id of the engineer to delete</param>
    /// <exception cref="BO.BlDoesNotExistException">this engineer is not exist</exception>
    public void RemoveEngineer(int id)
    {
        DO.Engineer? engineer = _dal.Engineer.Read(id);
        if (engineer != null)
        {
            _dal.Engineer.Delete(id);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        }
    }
    /// <summary>
    /// read detailes of an engineer
    /// </summary>
    /// <param name="id">id of engineer to return</param>
    /// <returns>engineer delailes</returns>
    /// <exception cref="BO.BlDoesNotExistException">this engineer is not exist</exception>
    public BO.Engineer RequestEngineerDetails(int id)
    {
        DO.Engineer? engineer = _dal.Engineer.Read(id);
        if(engineer == null)
        {
            throw new BO.BlDoesNotExistException($"engineer.Id with ID={id} does Not exist");
        }
        BO.Engineer boEngineer = new BO.Engineer() 
        {
          Id = engineer!.Id,
          Name = engineer.Name,
          Email = engineer.Email,
          Level = (BO.EngineerExperience)engineer.Level,
          Cost = engineer.Cost,
        };
        return boEngineer;

    }
    /// <summary>
    /// read all engineer details
    /// </summary>
    /// <param name="filter">an option for sorting the list</param>
    /// <returns>list of all the engineers</returns>
    public IEnumerable<BO.Engineer>? RequestEngineersList(Func<BO.Engineer, bool>? filter = null)
    {
        var engineers = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                        select new BO.Engineer
                        {
                            Id = doEngineer.Id,
                            Name = doEngineer.Name,
                            Email = doEngineer.Email,
                            Level = (BO.EngineerExperience)doEngineer.Level,
                            Cost = doEngineer.Cost,
                        });
        if(filter != null)
        {
            IEnumerable<BO.Engineer> engineersFilter = (from item in engineers
                                   where filter(item)
                                       select item);
            return engineersFilter;
        }

        return engineers;

    }
    /// <summary>
    /// update detales of an engineer
    /// </summary>
    /// <param name="engineer">the engineer with the new detailes</param>
    /// <exception cref="BO.BlDoesNotExistException">the id of this engineer is not exist</exception>
    public void UpdateEngineerDetails(BO.Engineer engineer)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(engineer.Id);
        if(doEngineer != null)
        {
            _dal.Engineer.Update(doEngineer);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={engineer.Id} does Not exist");
        }

    }
}


namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public void AddEngineer(Engineer engineer)
    {
        DO.Engineer engineer1 =
            new DO.Engineer(engineer.Id, engineer.Name!, engineer.Email!,
            (DO.EngineerExperience)engineer.Level, engineer.Cost);
        try
        {

        }
        throw new NotImplementedException();
    }

    public void RemoveEngineer(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer RequestEngineerDetails(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer>? RequestEngineersList(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public void UpdateEngineerDetails(Engineer engineer)
    {
        throw new NotImplementedException();
    }
}

﻿

namespace BlImplementation;

using BlApi;
/// <summary>
/// call all the defenitions of the interfaces
/// </summary>
internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMilestone Milestone => new MilestoneImplementation();

    public ITask Task => new TaskImplementation();

    public void InitializeDB() => DalTest.Initialization.Do();
}

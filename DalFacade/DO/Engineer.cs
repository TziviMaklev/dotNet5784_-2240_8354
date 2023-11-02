namespace DO;
/// <summary>
/// details about any of the engineers
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level">the level of the engineer (to now what tasks he can do)</param>
/// <param name="Cost">for an hour</param>
public record Engineer
(
    int Id,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost
)   
{
    public Engineer() : this(0, "", "", 0, 0.0) { }
    //empty ctor for stage 3
}




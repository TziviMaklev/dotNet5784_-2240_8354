
namespace BlApi;
/// <summary>
/// a factory for making only one data base
/// </summary>
public static class Factory
{
    public static IBl Get => new BlImplementation.Bl();

}


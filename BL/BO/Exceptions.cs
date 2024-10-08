﻿
namespace BO;
[Serializable]

public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
    public BlNullPropertyException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}
public class BlChoiseDoesNotExistException : Exception//if the choise that the user chose is not exist
{
    public BlChoiseDoesNotExistException(string? message) : base(message) { }
}
public class BlTimeSurfing : Exception
{
    public BlTimeSurfing(string? message) : base(message) { }
}
public class ChoiseDoesNotExistException : Exception
{
    public ChoiseDoesNotExistException(string? message) : base(message) { }
}
public class ThePogramNotSuccedToConvert : Exception
{
    public ThePogramNotSuccedToConvert(string? message) : base(message) { }
}
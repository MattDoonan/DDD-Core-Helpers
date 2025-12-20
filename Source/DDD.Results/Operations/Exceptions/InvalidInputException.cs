using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Represents an exception for invalid input operation status.
/// </summary>
public class InvalidInputException : OperationException
{
    public InvalidInputException(InvalidInput failure) : base(failure)
    {
    }

    public InvalidInputException(InvalidInput failure, string message) : base(failure, message)
    {
    }

    public InvalidInputException(InvalidInput failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
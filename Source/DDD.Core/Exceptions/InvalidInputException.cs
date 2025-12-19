using DDD.Core.Statuses;
using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

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
using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an operation fails due to an invalid request.
/// </summary>
public class InvalidRequestException : OperationException
{
    public InvalidRequestException(FailedOperationStatus failure) : base(failure)
    {
    }

    public InvalidRequestException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public InvalidRequestException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
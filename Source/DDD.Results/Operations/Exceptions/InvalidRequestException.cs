using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an operation fails due to an invalid request.
/// </summary>
public class InvalidRequestException : OperationException
{
    public InvalidRequestException() : base(OperationStatus.InvalidRequest())
    {
    }
    
    public InvalidRequestException(InvalidRequest failure) : base(failure)
    {
    }

    public InvalidRequestException(InvalidRequest failure, string message) : base(failure, message)
    {
    }

    public InvalidRequestException(InvalidRequest failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
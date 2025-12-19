using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an attempt is made to create an entity that already exists.
/// </summary>
public class AlreadyExistsException : OperationException
{
    public AlreadyExistsException(FailedOperationStatus failure) : base(failure)
    {
    }

    public AlreadyExistsException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public AlreadyExistsException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
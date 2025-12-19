using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : OperationException
{
    public NotFoundException(FailedOperationStatus failure) : base(failure)
    {
    }

    public NotFoundException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public NotFoundException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an operation is not allowed.
/// </summary>
public class NotAllowedException : OperationException
{
    public NotAllowedException(FailedOperationStatus failure) : base(failure)
    {
    }

    public NotAllowedException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public NotAllowedException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
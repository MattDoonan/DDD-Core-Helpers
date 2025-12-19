using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when a concurrency violation occurs.
/// </summary>
public class ConcurrencyViolationException : OperationException
{
    public ConcurrencyViolationException(FailedOperationStatus failure) : base(failure)
    {
    }

    public ConcurrencyViolationException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public ConcurrencyViolationException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
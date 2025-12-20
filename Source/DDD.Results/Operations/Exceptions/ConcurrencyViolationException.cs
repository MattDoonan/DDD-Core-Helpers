using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when a concurrency violation occurs.
/// </summary>
public class ConcurrencyViolationException : OperationException
{
    public ConcurrencyViolationException(ConcurrencyViolation failure) : base(failure)
    {
    }

    public ConcurrencyViolationException(ConcurrencyViolation failure, string message) : base(failure, message)
    {
    }

    public ConcurrencyViolationException(ConcurrencyViolation failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
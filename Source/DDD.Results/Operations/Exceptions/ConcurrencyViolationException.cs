using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when a concurrency violation occurs.
/// </summary>
public class ConcurrencyViolationException : OperationException
{
    public ConcurrencyViolationException() : base(OperationStatus.ConcurrencyViolation())
    {
    }
    
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
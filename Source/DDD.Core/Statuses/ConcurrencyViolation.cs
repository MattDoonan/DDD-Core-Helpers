using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a concurrency violation operation status.
/// </summary>
public record ConcurrencyViolation : FailedOperationStatus
{
    internal ConcurrencyViolation() 
        : base(StatusType.ConcurrencyViolation, "The operation failed due to a concurrency violation")
    {
    }
    
    internal ConcurrencyViolation(Type expectedType) 
        : base(StatusType.ConcurrencyViolation, $"The operation retrieving {expectedType.Name} failed due to a concurrency violation")
    {
    }
    
    protected ConcurrencyViolation(string message) 
        : base(StatusType.ConcurrencyViolation, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new ConcurrencyViolationException(this);
    }
}


/// <summary>
/// Generic version of ConcurrencyViolation status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type of the resource related to the concurrency violation.
/// </typeparam>
public record ConcurrencyViolation<T> : ConcurrencyViolation
{
    internal ConcurrencyViolation() : base(typeof(T))
    {
    }
    
    protected ConcurrencyViolation(string message) 
        : base(message)
    {
    }
}
using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents a concurrency violation operation status.
/// </summary>
public record ConcurrencyViolation : FailedOperationStatus
{
    internal ConcurrencyViolation() 
        : this("The operation failed due to a concurrency violation")
    {
    }
    
    protected ConcurrencyViolation(Type expectedType) 
        : this(expectedType, $"The operation retrieving {expectedType.Name} failed due to a concurrency violation")
    {
    }
    
    protected ConcurrencyViolation(string message) 
        : base(StatusType.ConcurrencyViolation, message)
    {
    }
    
    protected ConcurrencyViolation(Type expectedType, string message) 
        : base(StatusType.ConcurrencyViolation, expectedType, message)
    {
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
        : base(typeof(T), message)
    {
    }
}
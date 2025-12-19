using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a concurrency violation operation status.
/// </summary>
public record ConcurrencyViolation : FailedOperationStatus
{
    public ConcurrencyViolation() 
        : base(StatusType.ConcurrencyViolation, "The operation failed due to a concurrency violation")
    {
    }
    
    public ConcurrencyViolation(Type expectedType) 
        : base(StatusType.ConcurrencyViolation, $"The operation retrieving {expectedType.Name} failed due to a concurrency violation")
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
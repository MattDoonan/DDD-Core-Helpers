using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a failed operation status.
/// </summary>
public record Failure : FailedOperationStatus
{
    public Failure() : base(StatusType.GenericFailure, "The operation was a failure")
    {
    }
    
    public Failure(Type expectedType) 
        : base(StatusType.GenericFailure, $"The operation to retrieve {expectedType.Name} was a failure")
    {
    }
    
    public override void Throw()
    {
        throw ToException();
    }
    
    public override OperationException ToException()
    {
        return new OperationException(this);
    }
}
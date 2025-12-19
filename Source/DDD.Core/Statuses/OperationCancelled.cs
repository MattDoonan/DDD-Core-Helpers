using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation was cancelled.
/// </summary>
public record OperationCancelled : FailedOperationStatus
{
    public OperationCancelled() 
        : base(StatusType.OperationCancelled, "The operation was cancelled")
    {
    }
    protected OperationCancelled(Type expectedType) 
        : base(StatusType.OperationCancelled, $"The operation to retrieve {expectedType.Name} was cancelled")
    {
    }
    
    public override void Throw()
    {
        throw ToException();
    }
    
    public override OperationException ToException()
    {
        return new OperationCancelledException(this);
    }
}
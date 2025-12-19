using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation has timed out.
/// </summary>
public record OperationTimeout : FailedOperationStatus
{
    public OperationTimeout() 
        : base(StatusType.OperationTimeout, "The operation has timed out")
    {
    }
    
    protected OperationTimeout(Type expectedType) 
        : base(StatusType.OperationTimeout, $"The operation to retrieve {expectedType.Name} has timed out")
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new OperationTimeoutException(this);
    }
}
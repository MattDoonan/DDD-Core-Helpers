using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation was cancelled.
/// </summary>
public record Cancelled : FailedOperationStatus
{
    internal Cancelled() 
        : base(StatusType.OperationCancelled, "The operation was cancelled")
    {
    }
    internal Cancelled(Type expectedType) 
        : base(StatusType.OperationCancelled, $"The operation to retrieve {expectedType.Name} was cancelled")
    {
    }
    
    protected Cancelled(string message) 
        : base(StatusType.OperationCancelled, message)
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

/// <summary>
/// Generic version of Cancelled status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type of the resource related to the cancelled operation.
/// </typeparam>
public record Cancelled<T> : Cancelled
{
    internal Cancelled() : base(typeof(T))
    {
    }
    
    protected Cancelled(string message) 
        : base(message)
    {
    }
}
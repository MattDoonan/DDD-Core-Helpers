using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation was cancelled.
/// </summary>
public record Cancelled : FailedOperationStatus
{
    internal Cancelled() 
        : this("The operation was cancelled")
    {
    }
    protected Cancelled(Type expectedType) 
        : this(expectedType, $"The operation to retrieve {expectedType.Name} was cancelled")
    {
    }
    
    protected Cancelled(string message) 
        : base(StatusType.OperationCancelled, message)
    {
    }
    
    protected Cancelled(Type expectedOutput, string message) 
        : base(StatusType.OperationCancelled, expectedOutput, message)
    {
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
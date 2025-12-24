using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation has timed out.
/// </summary>
public record TimedOut : FailedOperationStatus
{
    internal TimedOut() 
        : this("The operation has timed out")
    {
    }
    
    protected TimedOut(Type expectedType) 
        : this(expectedType, $"The operation to retrieve {expectedType.Name} has timed out")
    {
    }
    
    protected TimedOut(string message) 
        : base(StatusType.OperationTimeout, message)
    {
    }
    
    protected TimedOut(Type expectedType, string message) 
        : base(StatusType.OperationTimeout, expectedType, message)
    {
    }

    public override OperationException ToException()
    {
        return new OperationTimeoutException(this);
    }
}

/// <summary>
/// Represents an operation status indicating that the operation to retrieve a value of type T has timed out.
/// </summary>
/// <typeparam name="T">
/// The type of the value that the operation was attempting to retrieve.
/// </typeparam>
public record TimedOut<T> : TimedOut
{
    internal TimedOut() 
        : base(typeof(T))
    {
    }
    
    protected TimedOut(string message) 
        : base(typeof(T), message)
    {
    }
}
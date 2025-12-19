using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation has timed out.
/// </summary>
public record TimedOut : FailedOperationStatus
{
    internal TimedOut() 
        : base(StatusType.OperationTimeout, "The operation has timed out")
    {
    }
    
    internal TimedOut(Type expectedType) 
        : base(StatusType.OperationTimeout, $"The operation to retrieve {expectedType.Name} has timed out")
    {
    }
    
    protected TimedOut(string message) 
        : base(StatusType.OperationTimeout, message)
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
        : base(message)
    {
    }
}
using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents a failed operation status.
/// </summary>
public record Failure : FailedOperationStatus
{
    internal Failure() 
        : this("The operation was a failure")
    {
    }
    
    protected Failure(Type expectedType) 
        : this(expectedType, $"The operation to retrieve {expectedType.Name} was a failure")
    {
    }
    
    protected Failure(string message) 
        : base(StatusType.GenericFailure, message)
    {
    }
    
    protected Failure(Type expectedType, string message) 
        : base(StatusType.GenericFailure, expectedType, message)
    {
    }
    
    public override OperationException ToException()
    {
        return new OperationException(this);
    }
}

/// <summary>
/// Generic version of Failure status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type associated with the failure.
/// </typeparam>
public record Failure<T> : Failure
{
    internal Failure() : base(typeof(T))
    {
    }
    
    protected Failure(string message) 
        : base(typeof(T), message)
    {
    }
}
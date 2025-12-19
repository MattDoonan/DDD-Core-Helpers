using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a failed operation status.
/// </summary>
public record Failure : FailedOperationStatus
{
    internal Failure() : base(StatusType.GenericFailure, "The operation was a failure")
    {
    }
    
    internal Failure(Type expectedType) 
        : base(StatusType.GenericFailure, $"The operation to retrieve {expectedType.Name} was a failure")
    {
    }
    
    protected Failure(string message) 
        : base(StatusType.GenericFailure, message)
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
        : base(message)
    {
    }
}
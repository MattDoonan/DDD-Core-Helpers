using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents a successful operation status.
/// </summary>
public record Success : OperationStatus
{
    internal Success() 
        : this("The operation completed successfully")
    {
    }
    
    internal Success(Type expectedType) 
        : this(expectedType, $"The operation retrieving {expectedType.Name} completed successfully")
    {
    }
    
    internal Success(string message) 
        : base(StatusType.Success, message)
    {
    }
    
    internal Success(Type expectedType, string message) 
        : base(StatusType.Success, expectedType, message)
    {
    }
}

/// <summary>
/// Represents a successful operation status that returns a value of type T.
/// </summary>
/// <typeparam name="T">
/// The type of the value returned by the successful operation.
/// </typeparam>
public record Success<T> : Success
{
    internal Success() 
        : base(typeof(T))
    {
    }

    protected Success(string message) 
        : base(typeof(T), message)
    {
    }
}
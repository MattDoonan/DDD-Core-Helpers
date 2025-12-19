using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a successful operation status.
/// </summary>
public record Success : OperationStatus
{
    internal Success() 
        : base(StatusType.Success, "The operation completed successfully")
    {
    }
    
    internal Success(Type expectedType) 
        : base(StatusType.Success, $"The operation retrieving {expectedType} completed successfully")
    {
    }
    
    internal Success(string message) 
        : base(StatusType.Success, message)
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

    internal Success(string message) 
        : base(message)
    {
    }
}
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a successful operation status.
/// </summary>
public record Success : OperationStatus
{
    public Success() 
        : base(StatusType.Success, "The operation completed successfully")
    {
    }
    
    public Success(Type expectedType) 
        : base(StatusType.Success, $"The operation retrieving {expectedType} completed successfully")
    {
    }
}
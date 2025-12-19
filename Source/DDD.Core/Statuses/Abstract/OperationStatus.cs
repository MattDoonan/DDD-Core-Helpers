using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses.Abstract;

/// <summary>
/// Represents the status of an operation.
/// </summary>
public abstract record OperationStatus
{
    public readonly StatusType Type;
    public string Message;
    
    protected OperationStatus(StatusType statusType, string message)
    {
        Type = statusType;
        Message = message;
    }
}
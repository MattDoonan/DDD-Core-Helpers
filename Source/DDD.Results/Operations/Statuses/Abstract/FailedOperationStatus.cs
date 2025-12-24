using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses.Abstract;

/// <summary>
/// Represents a failed operation status.
/// </summary>
public abstract record FailedOperationStatus : OperationStatus
{
    protected FailedOperationStatus(StatusType statusType, string message) 
        : base(statusType, message)
    {
    }
    
    protected FailedOperationStatus(StatusType statusType, Type outputType, string message) 
        : base(statusType, outputType, message)
    {
    }

    /// <summary>
    /// Throws the corresponding OperationException.
    /// </summary>
    public void Throw()
    {
        throw ToException();
    }
    
    /// <summary>
    /// Converts the failure status to an OperationException.
    /// </summary>
    /// <returns></returns>
    public abstract OperationException ToException();
}
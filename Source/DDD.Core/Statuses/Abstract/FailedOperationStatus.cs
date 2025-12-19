using DDD.Core.Exceptions;
using DDD.Core.Results.Exceptions;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses.Abstract;

/// <summary>
/// Represents a failed operation status.
/// </summary>
public abstract record FailedOperationStatus : OperationStatus
{
    protected FailedOperationStatus(StatusType statusType, string message) : base(statusType, message)
    {
    }
    
    /// <summary>
    /// Throws the corresponding OperationException.
    /// </summary>
    public abstract void Throw();
    
    /// <summary>
    /// Converts the failure status to an OperationException.
    /// </summary>
    /// <returns></returns>
    public abstract OperationException ToException();
}
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown for operation failures.
/// </summary>
public class OperationException : Exception
{
    public StatusType Type => _status.Type;
    private readonly FailedOperationStatus _status;
    
    public OperationException(FailedOperationStatus failure)
    {
        _status = failure;
    }

    public OperationException(FailedOperationStatus failure, string message) : base(message)
    {
        _status = failure;
    }

    public OperationException(FailedOperationStatus failure, string message, Exception inner) : base(message, inner)
    {
        _status = failure;
    }
    
    /// <summary>
    /// Converts the exception back to its corresponding operation status.
    /// </summary>
    /// <returns></returns>
    public OperationStatus ToStatus()
    {
        return _status;
    }
}
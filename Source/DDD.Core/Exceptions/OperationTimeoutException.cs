using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an operation times out.
/// </summary>
public class OperationTimeoutException : OperationException
{
    public OperationTimeoutException(FailedOperationStatus failure) : base(failure)
    {
    }

    public OperationTimeoutException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public OperationTimeoutException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
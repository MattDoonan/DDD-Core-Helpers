using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an operation is cancelled.
/// </summary>
public class OperationCancelledException : OperationException
{
    public OperationCancelledException(FailedOperationStatus failure) : base(failure)
    {
    }

    public OperationCancelledException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public OperationCancelledException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
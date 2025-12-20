using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an operation is cancelled.
/// </summary>
public class OperationCancelledException : OperationException
{
    public OperationCancelledException(Cancelled failure) : base(failure)
    {
    }

    public OperationCancelledException(Cancelled failure, string message) : base(failure, message)
    {
    }

    public OperationCancelledException(Cancelled failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
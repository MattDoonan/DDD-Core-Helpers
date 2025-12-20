using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an operation times out.
/// </summary>
public class OperationTimeoutException : OperationException
{
    public OperationTimeoutException(TimedOut failure) : base(failure)
    {
    }

    public OperationTimeoutException(TimedOut failure, string message) : base(failure, message)
    {
    }

    public OperationTimeoutException(TimedOut failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
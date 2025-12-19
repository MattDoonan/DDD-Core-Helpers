using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an invariant is violated.
/// </summary>
public class InvariantViolationException : OperationException
{
    public InvariantViolationException(FailedOperationStatus failure) : base(failure)
    {
    }

    public InvariantViolationException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public InvariantViolationException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
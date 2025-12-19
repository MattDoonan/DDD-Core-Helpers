using DDD.Core.Statuses;
using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when an invariant is violated.
/// </summary>
public class InvariantViolationException : OperationException
{
    public InvariantViolationException(InvariantViolation failure) : base(failure)
    {
    }

    public InvariantViolationException(InvariantViolation failure, string message) : base(failure, message)
    {
    }

    public InvariantViolationException(InvariantViolation failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
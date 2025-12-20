using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an invariant is violated.
/// </summary>
public class InvariantViolationException : OperationException
{
    public InvariantViolationException() : base(OperationStatus.InvariantViolation())
    {
    }
    
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
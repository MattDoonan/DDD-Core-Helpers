using DDD.Core.Statuses.Abstract;

namespace DDD.Core.Exceptions;

/// <summary>
/// Exception thrown when a domain rule is violated.
/// </summary>
public class DomainViolationException : OperationException
{
    public DomainViolationException(FailedOperationStatus failure) : base(failure)
    {
    }

    public DomainViolationException(FailedOperationStatus failure, string message) : base(failure, message)
    {
    }

    public DomainViolationException(FailedOperationStatus failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
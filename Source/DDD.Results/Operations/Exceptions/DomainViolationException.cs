using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when a domain rule is violated.
/// </summary>
public class DomainViolationException : OperationException
{
    public DomainViolationException() : base(OperationStatus.DomainViolation())
    {
    }
    
    public DomainViolationException(DomainViolation failure) : base(failure)
    {
    }

    public DomainViolationException(DomainViolation failure, string message) : base(failure, message)
    {
    }

    public DomainViolationException(DomainViolation failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
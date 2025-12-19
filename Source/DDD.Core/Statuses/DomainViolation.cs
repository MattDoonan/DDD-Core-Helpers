using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a domain violation operation status.
/// </summary>
public record DomainViolation : FailedOperationStatus
{
    internal DomainViolation() 
        : base(StatusType.DomainViolation, "A domain violation has occurred")
    {
    }
    
    internal DomainViolation(Type expectedType) 
        : base(StatusType.DomainViolation, $"A domain violation has occurred while retrieving {expectedType.Name}")
    {
    }
    
    protected DomainViolation(string message) 
        : base(StatusType.DomainViolation, message)
    {
    }

    public override void Throw()
    { 
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new DomainViolationException(this);
    }
}

/// <summary>
/// Generic version of DomainViolation status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type associated with the domain violation.
/// </typeparam>
public record DomainViolation<T> : DomainViolation
{
    internal DomainViolation() : base(typeof(T))
    {
    }
    
    protected DomainViolation(string message) 
        : base(message)
    {
    }
}
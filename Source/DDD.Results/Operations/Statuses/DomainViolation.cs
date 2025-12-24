using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents a domain violation operation status.
/// </summary>
public record DomainViolation : FailedOperationStatus
{
    internal DomainViolation() 
        : this("A domain violation has occurred")
    {
    }
    
    protected DomainViolation(Type expectedType) 
        : this( $"A domain violation has occurred while retrieving {expectedType.Name}")
    {
    }
    
    protected DomainViolation(string message) 
        : base(StatusType.DomainViolation, message)
    {
    }
    
    protected DomainViolation(Type expectedType, string message) 
        : base(StatusType.DomainViolation, expectedType, message)
    {
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
        : base(typeof(T), message)
    {
    }
}
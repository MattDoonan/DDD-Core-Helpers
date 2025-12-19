using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents a domain violation operation status.
/// </summary>
public record DomainViolation : FailedOperationStatus
{
    public DomainViolation() 
        : base(StatusType.DomainViolation, "A domain violation has occurred")
    {
    }
    
    protected DomainViolation(Type expectedType) 
        : base(StatusType.DomainViolation, $"A domain violation has occurred while retrieving {expectedType.Name}")
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
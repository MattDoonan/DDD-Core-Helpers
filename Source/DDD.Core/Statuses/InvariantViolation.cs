using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating an invariant violation.
/// </summary>
public record InvariantViolation : FailedOperationStatus
{
    public InvariantViolation() 
        : base(StatusType.InvariantViolation, "An unexpected failure occured")
    {
    }
    
    public InvariantViolation(Type expectedType) 
        : base(StatusType.InvariantViolation, $"An unexpected failure occured when retrieving {expectedType.Name}")
    {
    }
    
    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new InvariantViolationException(this);
    }
}
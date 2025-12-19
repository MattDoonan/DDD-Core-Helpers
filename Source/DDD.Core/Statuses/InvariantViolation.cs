using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating an invariant violation.
/// </summary>
public record InvariantViolation : FailedOperationStatus
{
    internal InvariantViolation() 
        : base(StatusType.InvariantViolation, "An unexpected failure occured")
    {
    }
    
    internal InvariantViolation(Type expectedType) 
        : base(StatusType.InvariantViolation, $"An unexpected failure occured when retrieving {expectedType.Name}")
    {
    }
    
    protected InvariantViolation(string message) 
        : base(StatusType.InvariantViolation, message)
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

/// <summary>
/// Represents an invariant violation operation status for a specific type.
/// </summary>
/// <typeparam name="T">
/// The type associated with the invariant violation.
/// </typeparam>
public record InvariantViolation<T> : InvariantViolation
{
    internal InvariantViolation() 
        : base(typeof(T))
    {
    }
    
    protected InvariantViolation(string message) 
        : base(message)
    {
    }
}
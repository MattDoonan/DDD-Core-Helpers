using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an operation status indicating an invariant violation.
/// </summary>
public record InvariantViolation : FailedOperationStatus
{
    internal InvariantViolation() 
        : this("An invariant condition was violated")
    {
    }
    
    protected InvariantViolation(Type expectedType) 
        : this(expectedType, $"An invariant condition was violated when retrieving {expectedType.Name}")
    {
    }
    
    protected InvariantViolation(string message) 
        : base(StatusType.InvariantViolation, message)
    {
    }
    
    protected InvariantViolation(Type expectedType, string message) 
        : base(StatusType.InvariantViolation, expectedType, message)
    {
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
        : base(typeof(T), message)
    {
    }
}
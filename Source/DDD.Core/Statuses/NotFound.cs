using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that a requested resource was not found.
/// </summary>
public record NotFound : FailedOperationStatus
{
    internal NotFound() 
        : base(StatusType.NotFound, "Requested resource was not found")
    {
    }
    
    internal NotFound(Type expectedType) 
        : base(StatusType.NotFound, $"Requested resource of type {expectedType.Name} was not found")
    {
    }
    
    protected NotFound(string message) 
        : base(StatusType.NotFound, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        throw new NotFoundException(this);
    }
}

/// <summary>
/// Represents a not found operation status for a specific type.
/// </summary>
/// <typeparam name="T">
/// The type associated with the not found status.
/// </typeparam>
public record NotFound<T> : NotFound
{
    internal NotFound() 
        : base(typeof(T))
    {
    }
    
    protected NotFound(string message) 
        : base(message)
    {
    }
}
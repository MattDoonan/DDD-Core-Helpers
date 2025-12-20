using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an operation status indicating that a requested resource was not found.
/// </summary>
public record NotFound : FailedOperationStatus
{
    internal NotFound() 
        : this("Requested resource was not found")
    {
    }
    
    internal NotFound(Type expectedType) 
        : this(expectedType, $"Requested resource of type {expectedType.Name} was not found")
    {
    }
    
    protected NotFound(string message) 
        : base(StatusType.NotFound, message)
    {
    }
    
    protected NotFound(Type expectedType, string message) 
        : base(StatusType.NotFound, expectedType, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new NotFoundException(this);
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
        : base(typeof(T), message)
    {
    }
}
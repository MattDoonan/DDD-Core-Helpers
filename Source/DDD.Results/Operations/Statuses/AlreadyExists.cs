using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Status indicating that a resource already exists.
/// </summary>
public record AlreadyExists : FailedOperationStatus
{
    internal AlreadyExists() 
        : this("Resource already exists")
    {
    }

    internal AlreadyExists(Type expectedType)
        : this(expectedType, $"Resource of type {expectedType.Name} already exists")
    {
    }
    
    protected AlreadyExists(string message) 
        : base(StatusType.AlreadyExists, message)
    {
    }
    
    protected AlreadyExists(Type expectedType, string message) 
        : base(StatusType.AlreadyExists, expectedType, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new AlreadyExistsException(this);
    }
}

/// <summary>
/// Generic version of AlreadyExists status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type of the resource that already exists.
/// </typeparam>
public record AlreadyExists<T> : AlreadyExists
{
    internal AlreadyExists() : base(typeof(T))
    {
    }
    
    protected AlreadyExists(string message) 
        : base(typeof(T), message)
    {
    }
}
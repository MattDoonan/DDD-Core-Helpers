using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Status indicating that a resource already exists.
/// </summary>
public record AlreadyExists : FailedOperationStatus
{
    internal AlreadyExists() 
        : base(StatusType.AlreadyExists, "Resource already exists")
    {
    }

    internal AlreadyExists(Type expectedType)
        : base(StatusType.AlreadyExists, $"Resource of type {expectedType.Name} already exists")
    {
        
    }
    
    protected AlreadyExists(string message) 
        : base(StatusType.AlreadyExists, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        throw new AlreadyExistsException(this);
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
        : base(message)
    {
    }
}
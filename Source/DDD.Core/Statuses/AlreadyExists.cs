using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Status indicating that a resource already exists.
/// </summary>
public record AlreadyExists : FailedOperationStatus
{
    public AlreadyExists() : base(StatusType.AlreadyExists, "Resource already exists")
    {
    }

    public AlreadyExists(Type expectedType)
        : base(StatusType.AlreadyExists, $"Resource of type {expectedType.Name} already exists")
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
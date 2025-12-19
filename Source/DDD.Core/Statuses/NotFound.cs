using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that a requested resource was not found.
/// </summary>
public record NotFound : FailedOperationStatus
{
    public NotFound() : base(StatusType.NotFound, "Requested resource was not found")
    {
    }
    
    public NotFound(Type expectedType) : base(StatusType.NotFound, $"Requested resource of type {expectedType.Name} was not found")
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
using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation is not allowed.
/// </summary>
public record NotAllowed : FailedOperationStatus
{
    public NotAllowed() : base(StatusType.NotAllowed, "Operation is not permitted")
    {
    }
    
    public NotAllowed(Type expectedType) : base(StatusType.NotAllowed, $"Operation to get {expectedType.Name} is not permitted")
    {
    }
    
    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        throw new NotAllowedException(this);
    }
}
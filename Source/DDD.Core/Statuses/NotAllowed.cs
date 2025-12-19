using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation is not allowed.
/// </summary>
public record NotAllowed : FailedOperationStatus
{
    internal NotAllowed() 
        : base(StatusType.NotAllowed, "Operation is not permitted")
    {
    }
    
    internal NotAllowed(Type expectedType) 
        : base(StatusType.NotAllowed, $"Operation to get {expectedType.Name} is not permitted")
    {
    }
    
    protected NotAllowed(string message) 
        : base(StatusType.NotAllowed, message)
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

/// <summary>
/// Represents a not allowed operation status for a specific type.
/// </summary>
/// <typeparam name="T">
/// The type associated with the not allowed status.
/// </typeparam>
public record NotAllowed<T> : NotAllowed
{
    internal NotAllowed() 
        : base(typeof(T))
    {
    }
    
    protected NotAllowed(string message) 
        : base(message)
    {
    }
}

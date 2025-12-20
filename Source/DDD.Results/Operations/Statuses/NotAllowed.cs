using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an operation status indicating that the operation is not allowed.
/// </summary>
public record NotAllowed : FailedOperationStatus
{
    internal NotAllowed() 
        : this("The operation is not permitted")
    {
    }
    
    internal NotAllowed(Type expectedType) 
        : this(expectedType, $"The operation to get {expectedType.Name} is not permitted")
    {
    }
    
    protected NotAllowed(string message) 
        : base(StatusType.NotAllowed, message)
    {
    }
    
    protected NotAllowed(Type expectedType, string message) 
        : base(StatusType.NotAllowed, expectedType, message)
    {
    }
    
    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new NotAllowedException(this);
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
        : base(typeof(T), message)
    {
    }
}

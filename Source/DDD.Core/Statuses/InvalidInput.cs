using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an invalid input operation status.
/// </summary>
public record InvalidInput : FailedOperationStatus
{
    internal InvalidInput()
        : base(StatusType.InvalidInput, "The input is invalid")
    {
    }
    
    internal InvalidInput(Type expectedType) 
        : base(StatusType.InvalidInput, $"The input for {expectedType.Name} is invalid")
    {
    }
    
    protected InvalidInput(string message) 
        : base(StatusType.InvalidInput, message)
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new InvalidInputException(this);
    }
}

/// <summary>
/// Generic version of InvalidInput status for specific types.
/// </summary>
/// <typeparam name="T">
/// The type of the input that is invalid.
/// </typeparam>
public record InvalidInput<T> : InvalidInput
{
    internal InvalidInput() : base(typeof(T))
    {
    }
    
    protected InvalidInput(string message) 
        : base(message)
    {
    }
}
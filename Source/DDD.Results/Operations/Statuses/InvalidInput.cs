using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an invalid input operation status.
/// </summary>
public record InvalidInput : FailedOperationStatus
{
    internal InvalidInput()
        : this("The input is invalid")
    {
    }
    
    internal InvalidInput(Type expectedType) 
        : this(expectedType, $"The input for {expectedType.Name} is invalid")
    {
    }
    
    protected InvalidInput(string message) 
        : base(StatusType.InvalidInput, message)
    {
    }
    
    protected InvalidInput(Type expectedType, string message) 
        : base(StatusType.InvalidInput, expectedType, message)
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
        : base(typeof(T), message)
    {
    }
}
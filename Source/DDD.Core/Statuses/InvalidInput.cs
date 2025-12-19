using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an invalid input operation status.
/// </summary>
public record InvalidInput : FailedOperationStatus
{
    public InvalidInput()
        : base(StatusType.InvalidInput, "The input is invalid")
    {
    }
    
    public InvalidInput(Type expectedType) 
        : base(StatusType.InvalidInput, $"The input for {expectedType.Name} is invalid")
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
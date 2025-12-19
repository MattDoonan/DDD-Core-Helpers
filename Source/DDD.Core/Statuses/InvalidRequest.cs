using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an invalid request operation status.
/// </summary>
public record InvalidRequest : FailedOperationStatus
{
    public InvalidRequest() 
        : base(StatusType.InvalidRequest, "The request is invalid")
    {
    }
    
    public InvalidRequest(Type expectedType) 
        : base(StatusType.InvalidRequest, $"The request for {expectedType.Name} is invalid")
    {
    }

    public override void Throw()
    {
        throw ToException();
    }

    public override OperationException ToException()
    {
        return new InvalidRequestException(this);
    }
}
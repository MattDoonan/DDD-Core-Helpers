using DDD.Core.Exceptions;
using DDD.Core.Statuses.Abstract;
using DDD.Core.Statuses.ValueObjects;

namespace DDD.Core.Statuses;

/// <summary>
/// Represents an invalid request operation status.
/// </summary>
public record InvalidRequest : FailedOperationStatus
{
    internal InvalidRequest() 
        : base(StatusType.InvalidRequest, "The request is invalid")
    {
    }
    
    internal InvalidRequest(Type expectedType) 
        : base(StatusType.InvalidRequest, $"The request for {expectedType.Name} is invalid")
    {
    }
    
    protected InvalidRequest(string message) 
        : base(StatusType.InvalidRequest, message)
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


/// <summary>
/// Represents an invalid request operation status for a specific type.
/// </summary>
/// <typeparam name="T">
/// The type associated with the invalid request.
/// </typeparam>
public record InvalidRequest<T> : InvalidRequest
{
    internal InvalidRequest() 
        : base(typeof(T))
    {
    }
    
    protected InvalidRequest(string message) 
        : base(message)
    {
    }
}
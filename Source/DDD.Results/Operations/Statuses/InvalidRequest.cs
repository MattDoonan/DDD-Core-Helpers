using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;

namespace DDD.Core.Operations.Statuses;

/// <summary>
/// Represents an invalid request operation status.
/// </summary>
public record InvalidRequest : FailedOperationStatus
{
    internal InvalidRequest() 
        : this("The request is invalid")
    {
    }
    
    internal InvalidRequest(Type expectedType) 
        : this(expectedType, $"The request for {expectedType.Name} is invalid")
    {
    }
    
    protected InvalidRequest(string message) 
        : base(StatusType.InvalidRequest, message)
    {
    }
    
    protected InvalidRequest(Type expectedType, string message) 
        : base(StatusType.InvalidRequest, expectedType, message)
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
        : base(typeof(T), message)
    {
    }
}
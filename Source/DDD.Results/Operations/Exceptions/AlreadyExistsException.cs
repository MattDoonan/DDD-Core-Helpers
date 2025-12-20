using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an attempt is made to create an entity that already exists.
/// </summary>
public class AlreadyExistsException : OperationException
{
    public AlreadyExistsException() : base(OperationStatus.AlreadyExists())
    {
    }
    
    public AlreadyExistsException(AlreadyExists failure) : base(failure)
    {
    }

    public AlreadyExistsException(AlreadyExists failure, string message) : base(failure, message)
    {
    }

    public AlreadyExistsException(AlreadyExists failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
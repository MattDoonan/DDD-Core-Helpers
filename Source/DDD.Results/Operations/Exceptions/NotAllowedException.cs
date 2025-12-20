using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an operation is not allowed.
/// </summary>
public class NotAllowedException : OperationException
{
    public NotAllowedException() : base(OperationStatus.NotAllowed())
    {
    }
    
    public NotAllowedException(NotAllowed failure) : base(failure)
    {
    }

    public NotAllowedException(NotAllowed failure, string message) : base(failure, message)
    {
    }

    public NotAllowedException(NotAllowed failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
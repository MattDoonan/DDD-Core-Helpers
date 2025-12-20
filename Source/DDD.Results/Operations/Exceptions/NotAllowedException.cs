using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when an operation is not allowed.
/// </summary>
public class NotAllowedException : OperationException
{
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
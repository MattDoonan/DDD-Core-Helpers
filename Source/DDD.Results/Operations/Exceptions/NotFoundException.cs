using DDD.Core.Operations.Statuses;

namespace DDD.Core.Operations.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : OperationException
{
    public NotFoundException(NotFound failure) : base(failure)
    {
    }

    public NotFoundException(NotFound failure, string message) : base(failure, message)
    {
    }

    public NotFoundException(NotFound failure, string message, Exception inner) : base(failure, message, inner)
    {
    }
}
using DDD.Core.Results.Interfaces;

namespace DDD.Core.Results.Exceptions;

public class FailedResultException : ResultException
{
    public FailedResultException(IResultStatus result) : base(result)
    {
    }

    public FailedResultException(IResultStatus result, string message) : base(result, message)
    {
    }

    public FailedResultException(IResultStatus result, string message, Exception inner) : base(result, message, inner)
    {
    }
}
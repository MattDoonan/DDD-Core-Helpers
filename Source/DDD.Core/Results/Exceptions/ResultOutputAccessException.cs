using DDD.Core.Results.Interfaces;

namespace DDD.Core.Results.Exceptions;

public class ResultOutputAccessException : ResultException
{
    public ResultOutputAccessException(IResultStatus result) : base(result)
    {
    }

    public ResultOutputAccessException(IResultStatus result, string message) : base(result, message)
    {
    }

    public ResultOutputAccessException(IResultStatus result, string message, Exception inner) : base(result, message, inner)
    {
    }
}
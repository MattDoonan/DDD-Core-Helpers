using DDD.Core.Results.Interfaces;

namespace DDD.Core.Results.Exceptions;

public class ResultConversionException : ResultException
{
    public ResultConversionException(IResultStatus result) : base(result)
    {
    }

    public ResultConversionException(IResultStatus result, string message) : base(result, message)
    {
    }

    public ResultConversionException(IResultStatus result, string message, Exception inner) : base(result, message, inner)
    {
    }
}
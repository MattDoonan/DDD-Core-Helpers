using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Exceptions;

public class ResultException : Exception
{
    public ResultLayer ResultLayer => _result.CurrentLayer;
    public FailureType FailureType => _result.CurrentFailureType;
    public IEnumerable<string> ErrorMessages => _result.ErrorMessages;
    public bool ResultWasSuccessful => _result.IsSuccessful;
    public bool ResultWasFailure => _result.IsFailure;

    private readonly IResultStatus _result;

    public ResultException(IResultStatus result)
    {
        _result = result;
    }

    public ResultException(IResultStatus result, string message) : base(message)
    {
        _result = result;
    }

    public ResultException(IResultStatus result, string message, Exception inner) : base(message, inner)
    {
        _result = result;
    }

    public Result ToResult()
    {
        return Result.From(_result);
    }
}
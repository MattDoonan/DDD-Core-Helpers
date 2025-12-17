using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Exceptions;

public class ResultException : Exception
{
    /// <summary>
    /// The layer at which the result was generated.
    /// </summary>
    public ResultLayer ResultLayer => _result.CurrentLayer;
    
    /// <summary>
    /// The primary failure type of the result.
    /// </summary>
    public FailureType FailureType => _result.PrimaryFailureType;
    
    /// <summary>
    /// The error messages associated with the result.
    /// </summary>
    public IEnumerable<string> ErrorMessages => _result.ErrorMessages;
    
    /// <summary>
    /// Indicates whether the result was successful.
    /// </summary>
    public bool ResultWasSuccessful => _result.IsSuccessful;
    
    /// <summary>
    /// Indicates whether the result was a failure.
    /// </summary>
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

    /// <summary>
    /// Converts the exception back to a Result object.
    /// </summary>
    /// <returns>
    /// The Result object representing the status encapsulated by the exception.
    /// </returns>
    public Result ToResult()
    {
        return Result.From(_result);
    }
}
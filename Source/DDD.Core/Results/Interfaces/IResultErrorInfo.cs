using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Interfaces;

public interface IResultErrorInfo
{
    public string MainError { get; }
    public IReadOnlyCollection<ResultError> Errors { get; }
    public IEnumerable<string> ErrorMessages { get; }
    bool ContainsErrorWith(ResultLayer failedLayer);
    bool ContainsErrorWith(FailureType failureType);
    void AddErrors(IEnumerable<ResultError> errors);
    void AddErrors(FailureType newPrimaryFailure, IEnumerable<ResultError> errors);
    void AddError(FailureType newPrimaryFailure = FailureType.Generic, params ResultError[] errors);
    public void AddErrorMessage(FailureType newPrimaryFailure = FailureType.Generic, params string[] messages);
    
}
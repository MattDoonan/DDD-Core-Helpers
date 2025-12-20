using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Interfaces;

public interface IResultErrorInfo
{
    public string MainError { get; }
    public IReadOnlyCollection<ResultError> Errors { get; }
    public IEnumerable<string> ErrorMessages { get; }
    bool ContainsErrorWith(ResultLayer failedLayer);
    bool ContainsErrorWith(StatusType statusType);
    void AddErrors(IEnumerable<ResultError> errors);
    void AddErrors(FailedOperationStatus newPrimaryStatus, IEnumerable<ResultError> errors);
    void AddError(FailedOperationStatus newPrimaryStatus, params ResultError[] errors);
    void AddErrorMessage(FailedOperationStatus newPrimaryFailure, params string[] messages);
    
}
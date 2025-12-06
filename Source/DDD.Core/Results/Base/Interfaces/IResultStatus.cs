using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Base.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public IReadOnlyCollection<ResultError> Errors { get; }
    public IEnumerable<string> ErrorMessages { get; }
    public void AddErrorMessage(params string[] messages);
    
}

public interface IResultStatus : IResultFailure
{
    public bool IsSuccessful => !IsFailure;
    public FailureType CurrentFailureType { get; }
    public ResultLayer CurrentLayer { get; }
    public string MainError { get; }
}
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Interfaces;

public interface IResultFailure
{
    public bool IsFailure { get; }
    public FailureType PrimaryFailureType { get; }
    void SetPrimaryFailure(FailureType failureType);
    bool IsPrimaryFailure(FailureType expectedFailure);
}
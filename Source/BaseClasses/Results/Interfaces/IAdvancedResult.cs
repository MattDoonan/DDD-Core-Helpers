using Outputs.Results.Abstract;

namespace Outputs.Results.Interfaces;

public interface IAdvancedResult <out TResult> : IResultStatusBase<TResult>
    where TResult : IResultStatus
{
    static abstract TResult Fail(FailureType failureType, string because);
}
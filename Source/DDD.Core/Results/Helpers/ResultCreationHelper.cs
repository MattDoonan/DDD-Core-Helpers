using DDD.Core.Results.Base.Interfaces;

namespace DDD.Core.Results.Helpers;

internal static class ResultCreationHelper
{
    public static TResult Merge<TResult, TMergeableResult>(params TMergeableResult[] results)
        where TResult: IResultStatus, IResultFactory<TResult>
        where TMergeableResult : IResultStatus
    {
        var allSuccessful = results.All(r => r.IsSuccessful);
        return allSuccessful
            ? CreateResult(TResult.Pass(), results)
            : CreateResult(TResult.Fail($"Not all {nameof(TResult)} were successful"), results);
    }
    
    private static TResult CreateResult<TResult, TMergeableResult>(TResult result, TMergeableResult[] results)
        where TResult: IResultStatus, IResultFactory<TResult>
        where TMergeableResult : IResultStatus
    {
        result.AddErrorMessage(results.SelectMany(r => r.ErrorMessages).ToArray());
        return result;
    }
}
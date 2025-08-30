using Core.Results.Base.Abstract;
using Core.Results.Base.Interfaces;

namespace Core.Results.Helpers;

internal static class ResultCreationHelper
{
    public static TResult Merge<TResult, TMergeableResult>(params TMergeableResult[] results)
        where TResult: ResultStatus, IResultFactory<TResult>
        where TMergeableResult : ResultStatus
    {
        var allSuccessful = results.All(r => r.IsSuccessful);
        return allSuccessful
            ? CreateResult(TResult.Pass(), results)
            : CreateResult(TResult.Fail($"Not all {nameof(TResult)} were successful"), results);
    }
    
    private static TResult CreateResult<TResult, TMergeableResult>(TResult result, TMergeableResult[] results)
        where TResult: ResultStatus, IResultFactory<TResult>
        where TMergeableResult : ResultStatus
    {
        result.Errors.AddRange(results.SelectMany(r => r.Errors));
        return result;
    }
}
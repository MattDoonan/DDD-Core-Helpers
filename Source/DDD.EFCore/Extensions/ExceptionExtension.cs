using DDD.Core.Results;

namespace DDD.Core.Extensions;

internal static class ExceptionExtension
{
    extension(Exception e)
    {
        public RepoResult<T> ToRepoResult<T>()
        {
            return e switch
            {
                DbUpdateConcurrencyException => RepoResult.ConcurrencyViolation<T>($"of this error: {e.Message}"),
                DbUpdateException => RepoResult.InvalidRequest<T>($"the update operation failed because of {e.Message}"),
                OperationCanceledException => RepoResult.OperationCancelled<T>("the operation was cancelled"),
                _ => RepoResult.Fail<T>($"{e.Message}")
            };
        }
        
        public RepoResult ToRepoResult()
        {
            return e switch
            {
                DbUpdateConcurrencyException => RepoResult.ConcurrencyViolation($"of this error: {e.Message}"),
                DbUpdateException => RepoResult.InvalidRequest($"the update operation failed because of {e.Message}"),
                OperationCanceledException => RepoResult.OperationCancelled("the operation was cancelled"),
                _ => RepoResult.Fail($"{e.Message}")
            };
        }
    }
}
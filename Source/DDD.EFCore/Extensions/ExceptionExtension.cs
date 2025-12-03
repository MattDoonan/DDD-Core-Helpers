using DDD.Core.Results;

namespace DDD.Core.Extensions;

internal static class ExceptionExtension
{
    extension(Exception e)
    {
        public RepoResult<T> ToRepoResult<T>()
        {
            return ToRepoResult(e).ToTypedRepoResult<T>();
        }

        public RepoResult ToRepoResult()
        {
            return e switch
            {
                DbUpdateConcurrencyException => RepoResult.ConcurrencyViolation($"of this error: {e.Message}"),
                DbUpdateException => RepoResult.InvalidRequest($"the update operation failed because of {e.Message}"),
                _ => RepoResult.Fail($"{e.Message}")
            };
        }
    }
}
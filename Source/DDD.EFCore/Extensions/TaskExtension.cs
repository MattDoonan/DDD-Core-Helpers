using DDD.Core.Results;

namespace DDD.Core.Extensions;

internal static class TaskExtension
{
    public static async Task<RepoResult<List<TSource>>> ToRepoResultAsync<TSource>(this Task<List<TSource>> task)
    {
        try
        {
            var result = await task;
            return RepoResult.Pass(result);
        }
        catch (Exception e)
        {
            return e.ToRepoResult<List<TSource>>();
        }
    }

    public static async Task<RepoResult<TSource>> ToRepoResultAsync<TSource>(this Task<TSource?> task)
    {
        try
        {
            var result = await task;
            return result is not null
                ? RepoResult.Pass(result)
                : RepoResult.NotFound<TSource>("there was no result obtained from the query");
        }
        catch (Exception e)
        {
            return e.ToRepoResult<TSource>();
        }
    }
}
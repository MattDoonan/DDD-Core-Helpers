using DDD.Core.Results;

namespace DDD.Core.Extensions;

internal static class TaskExtension
{
    public static async Task<RepoResult<List<TSource>>> ToTypedRepoResultAsync<TSource>(this Task<List<TSource>> task)
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

    public static async Task<RepoResult<TSource>> ToTypedRepoResultAsync<TSource>(this Task<TSource?> task)
    {
        try
        {
            var result = await task;
            return result is not null
                ? RepoResult.Pass(result)
                : RepoResult.NotFound<TSource>("there was no result obtained");
        }
        catch (Exception e)
        {
            return e.ToRepoResult<TSource>();
        }
    }
    
    public static async Task<RepoResult> ToRepoResultAsync(this Task task)
    {
        try
        {
            await task;
            return RepoResult.Pass();
        }
        catch (Exception e)
        {
            return e.ToRepoResult();
        }
    }
}
using DDD.Core.Extensions;
using DDD.Core.Results;

namespace DDD.Core.Repositories;

public class UnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    public UnitOfWork(TDbContext context)
    {
        Context = context;
    }

    public async Task<RepoResult> SaveAsync(CancellationToken token = default)
    {
        try
        {
            await Context.SaveChangesAsync(token);
            return RepoResult.Pass();
        }
        catch (Exception e)
        {
            return e.ToRepoResult();
        }
    }
}
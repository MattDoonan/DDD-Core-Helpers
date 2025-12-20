using DDD.Core.Extensions;
using DDD.Core.Results;

namespace DDD.Core.Repositories;

public class WriteUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    public WriteUnitOfWork(TDbContext context)
    {
        Context = context;
    }

    public Task<RepoResult> SaveAsync(CancellationToken token = default)
    {
        return Context.SaveAsync(token);
    }
    
    public void Add<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Add(aggregateRoot);
    }
    
    public void AddMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.AddRange(aggregateRoots);
    }

    public void Update<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Update(aggregateRoot);
    }

    public void UpdateMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.UpdateRange(aggregateRoots);
    }
    
}
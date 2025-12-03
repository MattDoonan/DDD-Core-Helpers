using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Repositories;

public class PersistenceRepository<T, TDbContext> : WriteRepository<T>
    where T : Entity, IAggregateRoot
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    public PersistenceRepository(TDbContext context) : base(context.Set<T>())
    {
        Context = context;
    }

    public async Task<RepoResult> AddAsync(T aggregateRoot, CancellationToken token = default)
    {
        Add(aggregateRoot);
        return await SaveAsync(token);
    }
    
    public async Task<RepoResult> AddManyAsync(IEnumerable<T> aggregateRoots, CancellationToken token = default)
    {
        AddMany(aggregateRoots);
        return await SaveAsync(token);
    }

    public async Task<RepoResult> UpdateAsync(T aggregateRoot, CancellationToken token = default)
    {
        Attach(aggregateRoot);
        return await SaveAsync(token);
    }
    
    public async Task<RepoResult> UpdateManyAsync(IEnumerable<T> aggregateRoots, CancellationToken token = default)
    {
        UpdateMany(aggregateRoots);
        return await SaveAsync(token);
    }
    
    public Task<RepoResult> SaveAsync(CancellationToken token = default)
    {
        return new UnitOfWork<TDbContext>(Context).SaveAsync(token);
    }
}
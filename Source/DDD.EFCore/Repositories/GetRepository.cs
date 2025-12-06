using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Queries;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Repositories;

public class GetRepository<TId, T> : QueryRepository<T>
    where TId : ValueObject, IAggregateRootId
    where T : Entity, IAggregateRoot<TId>
{
    private readonly DbSet<T> _dbSet;
    
    public GetRepository(DbSet<T> dbSet) 
    {
        _dbSet = dbSet;
    }

    public Task<RepoResult<T>> GetTrackedAsync(TId id, CancellationToken token = default)
    {
        return FindOneByPredicate(Get.ById<TId, T>(id), true, token);
    }
    
    public Task<RepoResult<T>> GetAsync(TId id, CancellationToken token = default)
    {
        return FindOneByPredicate(Get.ById<TId, T>(id), false, token);
    }
    
    public Task<RepoResult<List<T>>> GetAllAsync(CancellationToken token = default)
    {
        return FindManyByQuery(GetQueryable(), false, token);
    }

    protected override IQueryable<T> GetQueryable()
    {
        return _dbSet;
    }
    
}
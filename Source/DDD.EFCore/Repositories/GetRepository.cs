using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Queries;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Repositories;

/// <summary>
/// A repository for retrieving aggregate roots by their identifiers.
/// Inherits from QueryRepository to support querying capabilities.
/// </summary>
/// <typeparam name="TId">
/// The type of the aggregate root identifier.
/// </typeparam>
/// <typeparam name="T">
/// The type of the aggregate root.
/// </typeparam>
public class GetRepository<TId, T> : QueryRepository<T>
    where TId : ValueObject, IAggregateRootId
    where T : Entity, IAggregateRoot<TId>
{
    private readonly DbSet<T> _dbSet;
    
    public GetRepository(DbSet<T> dbSet) 
    {
        _dbSet = dbSet;
    }

    /// <summary>
    /// Retrieves a tracked aggregate root by its identifier.
    /// </summary>
    /// <param name="id">
    /// The identifier of the aggregate root to retrieve.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the retrieval operation, including the aggregate root if found.
    /// </returns>
    public Task<RepoResult<T>> GetTrackedAsync(TId id, CancellationToken token = default)
    {
        return FindOneByPredicate(Get.ById<TId, T>(id), true, token);
    }
    
    /// <summary>
    /// Retrieves an untracked aggregate root by its identifier.
    /// </summary>
    /// <param name="id">
    /// The identifier of the aggregate root to retrieve.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the retrieval operation, including the aggregate root if found.
    /// </returns>
    public Task<RepoResult<T>> GetAsync(TId id, CancellationToken token = default)
    {
        return FindOneByPredicate(Get.ById<TId, T>(id), false, token);
    }
    
    /// <summary>
    /// Retrieves all aggregate roots in an untracked state.
    /// </summary>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the retrieval operation, including the list of aggregate roots.
    /// </returns>
    public Task<RepoResult<List<T>>> GetAllAsync(CancellationToken token = default)
    {
        return FindManyByQuery(GetQueryable(), false, token);
    }

    /// <summary>
    /// Gets the queryable collection of aggregate roots.
    /// Override this method to customize the queryable
    /// and to include related entities as needed.
    /// </summary>
    /// <returns>
    /// The queryable for the aggregate root.
    /// </returns>
    protected override IQueryable<T> GetQueryable()
    {
        return _dbSet;
    }
    
}
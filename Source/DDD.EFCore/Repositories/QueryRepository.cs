using System.Linq.Expressions;
using DDD.Core.Extensions;
using DDD.Core.Results;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Repositories;

/// <summary>
/// Base class for query repositories
/// Provides methods to find entities by predicates or queries
/// Wraps exceptions in RepoResult
/// </summary>
/// <typeparam name="T">
/// The type of the entity
/// </typeparam>
public abstract class QueryRepository<T> : ISingleRepository
    where T : class
{
    
    /// <summary>
    /// Finds a single entity by a given predicate.
    /// </summary>
    /// <param name="predicate">
    /// The predicate to filter entities.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entity should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the find operation, including the entity if found.
    /// </returns>
    protected Task<RepoResult<T>> FindOneByPredicate(Expression<Func<T, bool>> predicate, bool hasTracking, CancellationToken token)
    {
        var fullQueryable = GetQueryable();
        var query = fullQueryable.Where(predicate);
        return FindOneByQuery(query, hasTracking, token);
    }
    
    /// <summary>
    /// Finds a single entity of a specific inheritor type by a given predicate.
    /// </summary>
    /// <param name="predicate">
    /// The predicate to filter entities.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entity should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <typeparam name="TInheritor">
    /// The type of the inheritor entity.
    /// </typeparam>
    /// <returns>
    /// A task that contains the result of the find operation, including the entity if found.
    /// </returns>
    protected Task<RepoResult<TInheritor>> FindOneByPredicate<TInheritor>(Expression<Func<TInheritor, bool>> predicate, bool hasTracking, CancellationToken token)
        where TInheritor : class, T
    {
        var fullQueryable = GetQueryable().OfType<TInheritor>();
        var query = fullQueryable.Where(predicate);
        return QueryRepository.FindFirstByQuery(query, hasTracking, token);
    }
    
    /// <summary>
    /// Finds multiple entities by a given predicate.
    /// </summary>
    /// <param name="predicate">
    /// The predicate to filter entities.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entities should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the find operation, including the list of entities if found.
    /// </returns>
    protected Task<RepoResult<List<T>>> FindManyByPredicate(Expression<Func<T, bool>> predicate, bool hasTracking, CancellationToken token)
    {
        var fullQueryable = GetQueryable();
        var query = fullQueryable.Where(predicate);
        return FindManyByQuery(query, hasTracking, token);
    }
    
    /// <summary>
    /// Finds multiple entities of a specific inheritor type by a given predicate.
    /// </summary>
    /// <param name="predicate">
    /// The predicate to filter entities.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entities should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <typeparam name="TInheritor">
    /// The type of the inheritor entity.
    /// </typeparam>
    /// <returns>
    /// A task that contains the result of the find operation, including the list of entities if found.
    /// </returns>
    protected Task<RepoResult<List<TInheritor>>> FindManyByPredicate<TInheritor>(Expression<Func<TInheritor, bool>> predicate, bool hasTracking, CancellationToken token)
        where TInheritor : class, T
    {
        var fullQueryable = GetQueryable().OfType<TInheritor>();
        var query = fullQueryable.Where(predicate);
        return QueryRepository.FindManyByQuery(query, hasTracking, token);
    }
    
    /// <summary>
    /// Finds a single entity by a given query.
    /// </summary>
    /// <param name="query">
    /// The query to execute.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entity should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the find operation, including the entity if found.
    /// </returns>
    protected Task<RepoResult<T>> FindOneByQuery(IQueryable<T> query, bool hasTracking, CancellationToken token)
    {
        return QueryRepository.FindFirstByQuery(query, hasTracking, token);

    }
    
    /// <summary>
    /// Finds multiple entities by a given query.
    /// </summary>
    /// <param name="query">
    /// The query to execute.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entities should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that contains the result of the find operation, including the list of entities if found.
    /// </returns>
    protected Task<RepoResult<List<T>>> FindManyByQuery(IQueryable<T> query, bool hasTracking, CancellationToken token)
    {
        return QueryRepository.FindManyByQuery(query, hasTracking, token);
    }

    /// <summary>
    /// Gets the base queryable for the entity type.
    /// Modify this method to provide the appropriate queryable from your data source.
    /// </summary>
    /// <returns>
    /// The base queryable for the entity type.
    /// </returns>
    protected abstract IQueryable<T> GetQueryable();
}

/// <summary>
/// Static helper class for query repository methods
/// </summary>
public static class QueryRepository
{
    /// <summary>
    /// Finds the first entity by a given query.
    /// </summary>
    /// <param name="query">
    /// The query to execute.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entity should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <typeparam name="T">
    /// The type of the entity.
    /// </typeparam>
    /// <returns>
    /// A task that contains the result of the find operation, including the entity if found.
    /// </returns>
    public static async Task<RepoResult<T>> FindFirstByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetFirstAsync(token);
    }
    
    /// <summary>
    /// Finds the last entity by a given query.
    /// </summary>
    /// <param name="query">
    /// The query to execute.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entity should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <typeparam name="T">
    /// The type of the entity.
    /// </typeparam>
    /// <returns>
    /// A task that contains the result of the find operation, including the entity if found.
    /// </returns>
    public static async Task<RepoResult<T>> FindLastByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetLastAsync(token);
    }
    
    /// <summary>
    /// Finds multiple entities by a given query.
    /// </summary>
    /// <param name="query">
    /// The query to execute.
    /// </param>
    /// <param name="hasTracking">
    /// Whether the entities should be tracked.
    /// </param>
    /// <param name="token">
    /// The cancellation token.
    /// </param>
    /// <typeparam name="T">
    /// The type of the entity.
    /// </typeparam>
    /// <returns>
    /// A task that contains the result of the find operation, including the list of entities if found.
    /// </returns>
    public static async Task<RepoResult<List<T>>> FindManyByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetManyAsync(token);
    }
}
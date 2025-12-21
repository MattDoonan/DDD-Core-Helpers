using System.Linq.Expressions;
using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
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
    
    protected Task<RepoResult<T>> FindOneByPredicate(Expression<Func<T, bool>> predicate, bool hasTracking, CancellationToken token)
    {
        var fullQueryable = GetQueryable();
        var query = fullQueryable.Where(predicate);
        return FindOneByQuery(query, hasTracking, token);
    }
    
    protected Task<RepoResult<TInheritor>> FindOneByPredicate<TInheritor>(Expression<Func<TInheritor, bool>> predicate, bool hasTracking, CancellationToken token)
        where TInheritor : class, T
    {
        var fullQueryable = GetQueryable().OfType<TInheritor>();
        var query = fullQueryable.Where(predicate);
        return QueryRepository.FindFirstByQuery(query, hasTracking, token);
    }
    
    protected Task<RepoResult<List<T>>> FindManyByPredicate(Expression<Func<T, bool>> predicate, bool hasTracking, CancellationToken token)
    {
        var fullQueryable = GetQueryable();
        var query = fullQueryable.Where(predicate);
        return FindManyByQuery(query, hasTracking, token);
    }
    
    protected Task<RepoResult<List<TInheritor>>> FindManyByPredicate<TInheritor>(Expression<Func<TInheritor, bool>> predicate, bool hasTracking, CancellationToken token)
        where TInheritor : class, T
    {
        var fullQueryable = GetQueryable().OfType<TInheritor>();
        var query = fullQueryable.Where(predicate);
        return QueryRepository.FindManyByQuery(query, hasTracking, token);
    }
    
    protected Task<RepoResult<T>> FindOneByQuery(IQueryable<T> query, bool hasTracking, CancellationToken token)
    {
        return QueryRepository.FindFirstByQuery(query, hasTracking, token);

    }
    
    protected Task<RepoResult<List<T>>> FindManyByQuery(IQueryable<T> query, bool hasTracking, CancellationToken token)
    {
        return QueryRepository.FindManyByQuery(query, hasTracking, token);
    }

    protected abstract IQueryable<T> GetQueryable();
}

public static class QueryRepository
{
    public static async Task<RepoResult<T>> FindFirstByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetFirstAsync(token);
    }
    
    public static async Task<RepoResult<T>> FindLastByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetLastAsync(token);
    }
    
    public static async Task<RepoResult<List<T>>> FindManyByQuery<T>(IQueryable<T> query, bool hasTracking, CancellationToken token)
        where T : class
    {
        return await query
            .ConfigureTracking(hasTracking)
            .GetManyAsync(token);
    }
}
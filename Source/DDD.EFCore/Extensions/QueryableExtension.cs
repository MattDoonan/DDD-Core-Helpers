using System.Linq.Expressions;
using DDD.Core.Results;

namespace DDD.Core.Extensions;

/// <summary>
/// Extensions for IQueryable to support repository query operations
/// </summary>
public static class QueryableExtension
{
    extension<TSource>(IQueryable<TSource> query) where TSource : class
    {
        /// <summary>
        /// Configures tracking for the queryable based on the hasTracking parameter
        /// </summary>
        /// <param name="hasTracking">
        /// If true, enables tracking; otherwise, disables tracking
        /// </param>
        /// <returns>
        /// The configured IQueryable with or without tracking
        /// </returns>
        public IQueryable<TSource> ConfigureTracking(bool hasTracking)
        {
            return hasTracking ? query.AsTracking() : query.AsNoTracking();
        }
        
        /// <summary>
        /// Gets the first entity matching the predicate from the queryable and wraps it in a RepoResult
        /// </summary>
        /// <param name="predicate">
        /// The predicate to filter entities
        /// </param>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the first entity or a failed result
        /// </returns>
        public Task<RepoResult<TSource>> GetFirstAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate);
            return fullQuery.GetFirstAsync(token);
        }
        
        /// <summary>
        /// Gets the first entity from the queryable and wraps it in a RepoResult
        /// </summary>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the first entity or a failed result
        /// </returns>
        public Task<RepoResult<TSource>> GetFirstAsync(CancellationToken token = default)
        {
            var fullQuery = query.FirstOrDefaultAsync(token);
            return fullQuery.ToTypedRepoResultAsync();
        }
        
        /// <summary>
        /// Gets the last entity matching the predicate from the queryable and wraps it in a RepoResult
        /// </summary>
        /// <param name="predicate">
        /// The predicate to filter entities
        /// </param>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the last entity or a failed result
        /// </returns>
        public Task<RepoResult<TSource>> GetLastAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate);
            return fullQuery.GetLastAsync(token);
        }
        
        /// <summary>
        /// Gets the last entity from the queryable and wraps it in a RepoResult
        /// </summary>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the last entity or a failed result
        /// </returns>
        public Task<RepoResult<TSource>> GetLastAsync(CancellationToken token = default)
        {
            var fullQuery = query.LastOrDefaultAsync(token);
            return fullQuery.ToTypedRepoResultAsync();
        }
        
        /// <summary>
        /// Gets all entities matching the predicate from the queryable and wraps them in a RepoResult
        /// </summary>
        /// <param name="predicate">
        /// The predicate to filter entities
        /// </param>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the list of entities or a failed result
        /// </returns>
        public Task<RepoResult<List<TSource>>> GetManyAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate);
            return fullQuery.GetManyAsync(token);
        }
        
        /// <summary>
        /// Gets all entities from the queryable and wraps them in a RepoResult
        /// </summary>
        /// <param name="token">
        /// Cancellation token to cancel the operation
        /// </param>
        /// <returns>
        /// A task that contains the RepoResult with the list of entities or a failed result
        /// </returns>
        public Task<RepoResult<List<TSource>>> GetManyAsync(CancellationToken token = default)
        {
            var fullQuery = query.ToListAsync(token);
            return fullQuery.ToTypedRepoResultAsync();
        }
    }
}
using System.Linq.Expressions;
using DDD.Core.Results;

namespace DDD.Core.Extensions;

public static class QueryableExtension
{
    extension<TSource>(IQueryable<TSource> query) where TSource : class
    {
        public IQueryable<TSource> ConfigureTracking(bool hasTracking)
        {
            return hasTracking ? query.AsTracking() : query.AsNoTracking();
        }
        
        public Task<RepoResult<TSource>> GetFirstAsync(CancellationToken token = default)
        {
            var fullQuery = query.FirstOrDefaultAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
        
        public Task<RepoResult<TSource>> GetFirstAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate).FirstOrDefaultAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
        
        public Task<RepoResult<TSource>> GetLastAsync(CancellationToken token = default)
        {
            var fullQuery = query.LastOrDefaultAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
        
        public Task<RepoResult<TSource>> GetLastAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate).LastOrDefaultAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
        
        public Task<RepoResult<List<TSource>>> GetManyAsync(CancellationToken token = default)
        {
            var fullQuery = query.ToListAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
        
        public Task<RepoResult<List<TSource>>> GetManyAsync(Expression<Func<TSource, bool>> predicate, CancellationToken token = default)
        {
            var fullQuery = query.Where(predicate).ToListAsync(token);
            return fullQuery.ToRepoResultAsync();
        }
    }
}
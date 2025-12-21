using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Repositories;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Extensions;

public static class DbContextExtensions
{
    extension(DbContext context)
    {
        /// <summary>
        /// Saves changes to the database and returns a RepoResult indicating success or failure.
        /// </summary>
        /// <param name="token">
        /// A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        /// A Task representing the asynchronous operation, containing a RepoResult.
        /// </returns>
        public Task<RepoResult> SaveAsync(CancellationToken token = default)
        {
            return context.SaveChangesAsync(token)
                .ToRepoResultAsync().RemoveTypeAsync();
        }
        
        public Task<RepoResult<TAggregate>> GetAsync<TId, TAggregate>(TId id, CancellationToken token = default)
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var repository = new GetRepository<TId, TAggregate>(context.Set<TAggregate>());
            return repository.GetAsync(id, token);
        }

        public Task<RepoResult<TAggregate>> GetTrackedAsync<TId, TAggregate>(TId id, CancellationToken token = default)
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var repository = new GetRepository<TId, TAggregate>(context.Set<TAggregate>());
            return repository.GetTrackedAsync(id, token);
        }
    }
}
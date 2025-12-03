using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Repositories;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Extensions;

public static class DbContextExtensions
{
    private static readonly Dictionary<Type, object> RepoCache = new();

    extension(DbContext context)
    {
        public GetRepository<TId, TAggregate> GenerateGetRepository<TId, TAggregate>()
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var type = typeof(TAggregate);
            
            if (RepoCache.TryGetValue(type, out var expectedRepo) 
                && expectedRepo is GetRepository<TId, TAggregate> aggregateRepository) 
                return aggregateRepository;
            
            var newRepository = new GetRepository<TId, TAggregate>(context.Set<TAggregate>());
            RepoCache[type] = newRepository;
            
            return newRepository;
        }
        
        public WriteRepository<TAggregate> GenerateWriteRepository<TAggregate>()
            where TAggregate : Entity, IAggregateRoot
        {
            var type = typeof(TAggregate);
            
            if (RepoCache.TryGetValue(type, out var expectedRepo) 
                && expectedRepo is WriteRepository<TAggregate> aggregateRepository) 
                return aggregateRepository;
            
            var newRepository = new WriteRepository<TAggregate>(context.Set<TAggregate>());
            RepoCache[type] = newRepository;
            
            return newRepository;
        }
        
        public Task<RepoResult<TAggregate>> GetAsync<TId, TAggregate>(TId id, CancellationToken token = default)
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var repository = GenerateGetRepository<TId, TAggregate>(context);
            return repository.GetAsync(id, token);
        }

        public Task<RepoResult<TAggregate>> GetTrackedAsync<TId, TAggregate>(TId id, CancellationToken token = default)
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var repository = GenerateGetRepository<TId, TAggregate>(context);
            return repository.GetTrackedAsync(id, token);
        }
    }
}
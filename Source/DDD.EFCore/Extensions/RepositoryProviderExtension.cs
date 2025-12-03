using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Repositories;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Extensions;

public static class RepositoryProviderExtension
{
    private static readonly Dictionary<string, object> RepoCache = new();

    extension(DbContext context)
    {
        public GetRepository<TId, TAggregate> CreateGetRepository<TId, TAggregate>()
            where TId : ValueObject, IAggregateRootId
            where TAggregate : Entity, IAggregateRoot<TId>
        {
            var name = CreateRepoName(typeof(GetRepository<TId, TAggregate>));

            if (RepoCache.TryGetValue(name, out var expectedRepo)
                && expectedRepo is GetRepository<TId, TAggregate> aggregateRepository)
                return aggregateRepository;

            var newRepository = new GetRepository<TId, TAggregate>(context.Set<TAggregate>());
            RepoCache[name] = newRepository;

            return newRepository;
        }

        public WriteRepository<TAggregate> CreateWriteRepository<TAggregate>()
            where TAggregate : Entity, IAggregateRoot
        {
            var name = CreateRepoName(typeof(WriteRepository<TAggregate>));

            if (RepoCache.TryGetValue(name, out var expectedRepo)
                && expectedRepo is WriteRepository<TAggregate> aggregateRepository)
                return aggregateRepository;

            var newRepository = new WriteRepository<TAggregate>(context.Set<TAggregate>());
            RepoCache[name] = newRepository;

            return newRepository;
        }
    }

    private static string CreateRepoName(Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var baseName = type.Name[..type.Name.IndexOf('`')];
        var args = type.GetGenericArguments()
            .Select(t => t.Name);

        return $"{baseName}<{string.Join(", ", args)}>";
    }
}
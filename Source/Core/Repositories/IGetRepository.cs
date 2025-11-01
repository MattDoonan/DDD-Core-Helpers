using Core.Entities.AggregateRoot;
using Core.Results.Advanced;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Repositories;

public interface IBasicGetRepository<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    RepoResult<T> Get(TId id);
}

public interface IBasicGetRepositoryAsync<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    Task<RepoResult<T>> GetAsync(TId id, CancellationToken token = default);
}
using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicGetRepository<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    RepoResult<T> Get(TId id);
}

public interface IAsyncBasicGetRepository<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    Task<RepoResult<T>> GetAsync(TId id, CancellationToken token = default);
}
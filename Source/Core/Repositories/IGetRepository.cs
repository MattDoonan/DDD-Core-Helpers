using Core.Entities.AggregateRoot;
using Core.Results.Basic;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Repositories;

public interface IBasicGetRepository<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    EntityResult<T> Get(TId id);
}

public interface IBasicGetRepositoryAsync<in TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    Task<EntityResult<T>> GetAsync(TId id, CancellationToken token = default);
}
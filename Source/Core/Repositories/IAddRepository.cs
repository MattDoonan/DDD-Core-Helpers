using Core.Entities.AggregateRoot;
using Core.Results.Basic;

namespace Core.Repositories;

public interface IBasicAddRepository<T>
    where T : IAggregateRoot
{
    EntityResult<T> Add(T aggregateRoot, CancellationToken token = default);
}

public interface IBasicAddRepositoryAsync<T>
    where T : IAggregateRoot
{
    Task<EntityResult<T>> AddAsync(T aggregateRoot, CancellationToken token = default);
}
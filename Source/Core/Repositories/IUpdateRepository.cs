using Core.Entities.AggregateRoot;
using Core.Results.Basic;

namespace Core.Repositories;

public interface IBasicUpdateRepository<T>
    where T : IAggregateRoot
{
    EntityResult<T> Update(T aggregateRoot);
}

public interface IBasicUpdateRepositoryAsync<T>
    where T : IAggregateRoot
{
    Task<EntityResult<T>> UpdateAsync(T aggregateRoot, CancellationToken token = default);
}
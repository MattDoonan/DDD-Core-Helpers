using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicUpdateRepository<T>
    where T : IAggregateRoot
{
    RepoResult<T> Update(T aggregateRoot);
}

public interface IBasicUpdateRepositoryAsync<T>
    where T : IAggregateRoot
{
    Task<RepoResult<T>> UpdateAsync(T aggregateRoot, CancellationToken token = default);
}
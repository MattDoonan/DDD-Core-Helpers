using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicAddRepository<T>
    where T : IAggregateRoot
{
    RepoResult<T> Add(T aggregateRoot);
}

public interface IBasicAddRepositoryAsync<T>
    where T : IAggregateRoot
{
    Task<RepoResult<T>> AddAsync(T aggregateRoot, CancellationToken token = default);
}
using Core.Entities.AggregateRoot;
using Core.Results.Advanced;
using Core.Results.Basic;

namespace Core.Repositories;

public interface IBasicAddRepository<T>
    where T : IAggregateRoot
{
    RepoResult<T> Add(T aggregateRoot, CancellationToken token = default);
}

public interface IBasicAddRepositoryAsync<T>
    where T : IAggregateRoot
{
    Task<RepoResult<T>> AddAsync(T aggregateRoot, CancellationToken token = default);
}
using Core.Entities.AggregateRoot;
using Core.Results.Advanced;

namespace Core.Repositories;

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
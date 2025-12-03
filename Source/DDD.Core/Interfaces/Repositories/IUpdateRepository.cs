using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicUpdateRepository<in T>
    where T : IAggregateRoot
{
    void Update(T aggregateRoot);
}

public interface IBasicUpdateManyRepository<in T>
    where T : IAggregateRoot
{
    void UpdateMany(IEnumerable<T> aggregateRoots);
}


public interface IAsyncBasicUpdateRepository<T>
    where T : IAggregateRoot
{
    Task<RepoResult<T>> UpdateAsync(T aggregateRoot, CancellationToken token = default);
}
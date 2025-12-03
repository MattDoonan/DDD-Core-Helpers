using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicAddRepository<in T>
    where T : IAggregateRoot
{
    void Add(T aggregateRoot);
}

public interface IBasicAddManyRepository<in T>
    where T : IAggregateRoot
{
    void AddMany(IEnumerable<T> aggregateRoots);
}

public interface IAsyncBasicAddRepository<T>
    where T : IAggregateRoot
{
    Task<RepoResult<T>> AddAsync(T aggregateRoot, CancellationToken token = default);
}
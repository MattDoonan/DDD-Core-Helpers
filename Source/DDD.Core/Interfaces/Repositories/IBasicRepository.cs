using DDD.Core.Entities.Interfaces;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Interfaces.Repositories;

public interface IBasicRepository<in TId, T> : IBasicAddRepository<T>, IBasicUpdateRepository<T>, IBasicGetRepository<TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId;
    
public interface IBasicRepositoryAsync<in TId, T> : IAsyncBasicAddRepository<T>, IAsyncBasicUpdateRepository<T>, IAsyncBasicGetRepository<TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId;
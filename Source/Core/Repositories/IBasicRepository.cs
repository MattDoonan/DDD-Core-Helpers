using Core.Entities.AggregateRoot;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Repositories;

public interface IBasicRepository<in TId, T> : IBasicAddRepository<T>, IBasicUpdateRepository<T>, IBasicGetRepository<TId, T>
    where T : IAggregateRoot<TId>
    where TId : IAggregateRootId;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.Interfaces;

public interface IAggregateRoot : IEntity;
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
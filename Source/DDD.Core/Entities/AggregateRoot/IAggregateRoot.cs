using DDD.Core.Entities.Regular;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.AggregateRoot;

public interface IAggregateRoot : IEntity;
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
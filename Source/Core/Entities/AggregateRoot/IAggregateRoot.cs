using Core.Entities.Regular;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Entities.AggregateRoot;

public interface IAggregateRoot : IEntity;
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
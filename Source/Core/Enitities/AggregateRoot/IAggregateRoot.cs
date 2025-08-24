using Core.Enitities.Regular;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Enitities.AggregateRoot;

public interface IAggregateRoot : IEntity;
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
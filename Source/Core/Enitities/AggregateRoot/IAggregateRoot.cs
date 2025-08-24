using Base.Enitities.Regular;
using Base.ValueObjects.AggregateRootIdentifiers.Base;

namespace Base.Enitities.AggregateRoot;

public interface IAggregateRoot : IEntity;
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
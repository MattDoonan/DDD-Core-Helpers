using Base.ObjectTypes;
using Logic.Entities.Regular;

namespace Logic.Entities.AggregateRoot;

public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
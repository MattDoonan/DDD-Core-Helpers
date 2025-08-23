using Base.ObjectTypes;
using Logic.Entities.Regular;

namespace Logic.Entities.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
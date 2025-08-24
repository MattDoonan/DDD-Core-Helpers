using Base.Enitities.Regular;
using Base.ValueObjects.AggregateRootIdentifiers.Base;

namespace Base.Enitities.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
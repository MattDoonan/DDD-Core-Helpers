using Core.Enitities.Regular;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Enitities.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
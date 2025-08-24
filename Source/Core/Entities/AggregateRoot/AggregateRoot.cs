using Core.Entities.Regular;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Entities.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
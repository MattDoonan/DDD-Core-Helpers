using Entities.Types.Regular;
using ValueObjects.Types.AggregateRootIdentifiers.Base;

namespace Entities.Types.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
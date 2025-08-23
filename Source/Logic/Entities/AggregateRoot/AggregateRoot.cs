using Entities.Regular;
using Outputs.ObjectTypes;

namespace Entities.AggregateRoot;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : IAggregateRootId;
using Entities.Regular;
using Outputs.ObjectTypes;

namespace Entities.AggregateRoot;

public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
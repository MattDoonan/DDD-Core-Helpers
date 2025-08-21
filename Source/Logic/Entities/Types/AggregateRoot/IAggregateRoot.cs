using Entities.Types.Regular;
using ValueObjects.Types.AggregateRootIdentifiers.Base;

namespace Entities.Types.AggregateRoot;

public interface IAggregateRoot : IEntity;

public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
    where TId : IAggregateRootId;
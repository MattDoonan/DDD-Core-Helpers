using DDD.Core.Entities.Regular;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.AggregateRoot;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    where TId : IAggregateRootId
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
    protected AggregateRoot()
    {
        
    }
    
}
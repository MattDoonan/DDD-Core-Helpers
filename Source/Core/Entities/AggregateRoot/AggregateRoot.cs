using Core.Entities.Regular;
using Core.ValueObjects.AggregateRootIdentifiers.Base;

namespace Core.Entities.AggregateRoot;

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
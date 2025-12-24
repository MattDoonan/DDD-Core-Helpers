using DDD.Core.Entities.Interfaces;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities;

/// <summary>
/// Base class for aggregate roots with identifier of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">
/// The type of the aggregate root identifier.
/// </typeparam>
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
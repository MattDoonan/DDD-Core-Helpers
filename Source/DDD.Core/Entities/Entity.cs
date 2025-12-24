using DDD.Core.Entities.Interfaces;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity;

/// <summary>
/// Base class for entities with identifier of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">
/// The type of the entity identifier.
/// </typeparam>
public abstract class Entity<TId> : Entity, IEntity<TId>
    where TId : IIdentifier
{
    public TId Id { get; init; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
        Id = default!;
    }
    
    public bool IsIdDefault()
    {
        return EqualityComparer<TId>.Default.Equals(Id, default);
    }
}
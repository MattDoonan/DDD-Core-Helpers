using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.Regular;

public abstract class Entity;

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
}
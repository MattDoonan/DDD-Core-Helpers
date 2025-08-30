using Core.ValueObjects.Identifiers.Base;

namespace Core.Entities.Regular;

public abstract class Entity<TId> : IEntity<TId>
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
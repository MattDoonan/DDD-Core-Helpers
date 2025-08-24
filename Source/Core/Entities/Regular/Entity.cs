using Core.ValueObjects.Identifiers.Base;

namespace Core.Entities.Regular;

public abstract class Entity<TId>(TId id) : IEntity<TId>
    where TId : IIdentifier
{
    public TId Id { get; } = id;
}
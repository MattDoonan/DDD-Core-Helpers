using ValueObjects.Types.Identifiers.Base;

namespace Entities.Types.Regular;

public abstract class Entity<TId>(TId id) : IEntity<TId>
    where TId : IIdentifier
{
    public TId Id { get; } = id;
}
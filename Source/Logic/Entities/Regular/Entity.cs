using Base.ObjectTypes;

namespace Logic.Entities.Regular;

public abstract class Entity<TId>(TId id) : IEntity<TId>
    where TId : IIdentifier
{
    public TId Id { get; } = id;
}
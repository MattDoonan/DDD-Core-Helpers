using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.Interfaces;

public interface IEntity;
public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
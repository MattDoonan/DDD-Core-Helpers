using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.Regular;

public interface IEntity;
public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
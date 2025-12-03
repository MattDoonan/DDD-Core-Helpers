using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Entities.Interfaces;

public interface IEntity;
public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    TId Id { get; }
}
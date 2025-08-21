using ValueObjects.Types.Identifiers.Base;

namespace Entities.Types.Regular;

public interface IEntity;

public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
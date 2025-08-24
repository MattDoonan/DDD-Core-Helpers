using Core.ValueObjects.Identifiers.Base;

namespace Core.Enitities.Regular;

public interface IEntity;
public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
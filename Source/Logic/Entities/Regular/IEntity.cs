using Base.ObjectTypes;

namespace Logic.Entities.Regular;

public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
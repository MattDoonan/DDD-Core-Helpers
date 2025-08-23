using Outputs.ObjectTypes;

namespace Entities.Regular;

public interface IEntity<out TId> : IEntity
    where TId : IIdentifier
{
    public TId Id { get; }
}
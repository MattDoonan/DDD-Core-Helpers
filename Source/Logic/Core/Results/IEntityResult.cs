using Core.Entities.Base;
using Outputs.Base;

namespace Core.Results;

public interface IEntityResult : IResultStatusBase<EntityResult>
{
    EntityResult Create(IResultStatusBase<EntityResult> status);
}

public interface IEntityResult<T> : IResultValueBase<T, EntityResult<T>> where T : class, IEntity
{
    EntityResult ToStatus();
}
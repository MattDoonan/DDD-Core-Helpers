using System.Linq.Expressions;
using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.ValueObjects.Base;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Queries;

internal static class Get
{
    public static Expression<Func<TAggregate, bool>> ById<TId, TAggregate>(TId id)
        where TId : ValueObject, IAggregateRootId
        where TAggregate : Entity, IAggregateRoot<TId>
    {
        return aggregate => aggregate.Id.Equals(id);
    }
}
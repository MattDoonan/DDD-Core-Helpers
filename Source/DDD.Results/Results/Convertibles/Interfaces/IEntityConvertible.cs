namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IEntityConvertible : IMapperConvertible
{
    EntityResult ToEntityResult();
    EntityResult<T> ToTypedEntityResult<T>();
}

public interface IEntityConvertible<T> : IMapperConvertible<T>, IEntityConvertible
{
    EntityResult<T> ToTypedEntityResult();
}
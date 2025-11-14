namespace DDD.Core.Results.Convertables.Interfaces;

public interface IEntityConvertable : IMapperConvertable
{
    public EntityResult ToEntityResult();
}

public interface IEntityConvertable<T> : IMapperConvertable<T>, IEntityConvertable
{
    public EntityResult<T> ToTypedEntityResult();
}
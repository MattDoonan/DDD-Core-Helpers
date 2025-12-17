namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IMapperResult
{}

public interface IMapperConvertible : IInfraConvertible, IMapperResult
{
    MapperResult ToMapperResult();
    MapperResult<T> ToTypedMapperResult<T>();
}

public interface IMapperConvertible<T> : IMapperConvertible, IInfraConvertible<T>
{
    MapperResult<T> ToTypedMapperResult();
}
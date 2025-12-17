namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IServiceConvertible : IUseCaseConvertible
{
    ServiceResult ToServiceResult();
    ServiceResult<T> ToTypedServiceResult<T>();
}

public interface IServiceConvertible<T> : IServiceConvertible, IUseCaseConvertible<T>
{
    ServiceResult<T> ToTypedServiceResult();
}
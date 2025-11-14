namespace DDD.Core.Results.Convertables.Interfaces;

public interface IServiceConvertable : IUseCaseConvertable
{
    ServiceResult ToServiceResult();
}

public interface IServiceConvertable<T> : IServiceConvertable, IUseCaseConvertable<T>
{
    ServiceResult<T> ToTypedServiceResult();
}
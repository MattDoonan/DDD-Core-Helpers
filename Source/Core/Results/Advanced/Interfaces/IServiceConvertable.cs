namespace Core.Results.Advanced.Interfaces;

public interface IServiceConvertable : IUseCaseConvertable
{
    ServiceResult ToServiceResult();
}

public interface IServiceConvertable<T> : IServiceConvertable, IUseCaseConvertable<T>
{
    ServiceResult<T> ToTypedServiceResult();
}
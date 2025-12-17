namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IUseCaseConvertible : IResultConvertible
{
    UseCaseResult ToUseCaseResult();
    UseCaseResult<T> ToTypedUseCaseResult<T>();
}

public interface IUseCaseConvertible<T> : IUseCaseConvertible, IResultConvertible<T>
{
    UseCaseResult<T> ToTypedUseCaseResult();
}
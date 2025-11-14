namespace DDD.Core.Results.Convertables.Interfaces;

public interface IUseCaseConvertable : IResultConvertable
{
    UseCaseResult ToUseCaseResult();
}

public interface IUseCaseConvertable<T> : IUseCaseConvertable, IResultConvertable<T>
{
    UseCaseResult<T> ToTypedUseCaseResult();
}
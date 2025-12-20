namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IInfraResult
{}

public interface IInfraConvertible : IRepoConvertible, IInfraResult
{
    InfraResult ToInfraResult();
    InfraResult<T> ToTypedInfraResult<T>();
}

public interface IInfraConvertible<T> : IInfraConvertible, IRepoConvertible<T>
{
    InfraResult<T> ToTypedInfraResult();
}
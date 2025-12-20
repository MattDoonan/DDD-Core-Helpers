namespace DDD.Core.Results.Convertibles.Interfaces;

public interface IRepoInheritor
{}

public interface IRepoConvertible : IServiceConvertible, IRepoInheritor
{
    RepoResult ToRepoResult();
    RepoResult<T> ToTypedRepoResult<T>();
}

public interface IRepoConvertible<T> : IRepoConvertible, IServiceConvertible<T>
{
    RepoResult<T> ToTypedRepoResult();
}
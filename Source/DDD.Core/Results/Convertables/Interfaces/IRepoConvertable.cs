namespace DDD.Core.Results.Convertables.Interfaces;

public interface IRepoConvertable : IServiceConvertable
{
    public RepoResult ToRepoResult();
}

public interface IRepoConvertable<T> : IRepoConvertable, IServiceConvertable<T>
{
    public RepoResult<T> ToTypedRepoResult();
}
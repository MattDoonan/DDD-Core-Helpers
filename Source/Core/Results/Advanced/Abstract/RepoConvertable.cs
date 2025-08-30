using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;

namespace Core.Results.Advanced.Abstract;

public abstract class RepoConvertable : ServiceConvertable, IRepoConvertable
{
    protected RepoConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected RepoConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected RepoConvertable(IRepoConvertable result) : base(result)
    {
    }
    
    protected RepoConvertable()
    {
    }
    
    public RepoResult ToRepoResult()
    {
        return RepoResult.Create(this);
    }
    
    public static implicit operator RepoResult(RepoConvertable result)
    {
        return result.ToRepoResult();
    }
}

public abstract class RepoConvertable<T> : ServiceConvertable<T>, IRepoConvertable<T>
{
    protected RepoConvertable(T value) : base(value)
    {
    }

    protected RepoConvertable(IRepoConvertable valueResult) : base(valueResult)
    {
    }

    protected RepoConvertable(IRepoConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected RepoConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected RepoConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    
    public RepoResult<T> ToTypedRepoResult()
    {
        return RepoResult<T>.Create(this);
    }

    public RepoResult ToRepoResult()
    {
        return RepoResult.Create(this);
    }
    
    public static implicit operator RepoResult<T>(RepoConvertable<T> result)
    {
        return result.ToTypedRepoResult();
    }
    
    public static implicit operator RepoResult(RepoConvertable<T> result)
    {
        return result.ToRepoResult();
    }
}
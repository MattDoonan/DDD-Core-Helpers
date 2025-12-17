using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class RepoConvertible : ServiceConvertible, IRepoConvertible
{
    
    protected RepoConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected RepoConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected RepoConvertible(ResultLayer resultLayer)  
        : base(resultLayer)
    {
    }
    
    public RepoResult ToRepoResult()
    {
        return RepoResult.From(this);
    }
    
    public RepoResult<T> ToTypedRepoResult<T>()
    {
        return RepoResult<T>.From(this);
    }
    
    public static implicit operator RepoResult(RepoConvertible result)
    {
        return result.ToRepoResult();
    }
}

public abstract class RepoConvertible<T> : ServiceConvertible<T>, IRepoConvertible<T>
{
    protected RepoConvertible(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected RepoConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected RepoConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected RepoConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }
    
    public RepoResult<T2> ToTypedRepoResult<T2>()
    {
        return RepoResult<T2>.From(this);
    }
    
    public RepoResult<T> ToTypedRepoResult()
    {
        return RepoResult<T>.From(this);
    }

    public RepoResult ToRepoResult()
    {
        return RepoResult.From((IServiceConvertible)this);
    }
    
    public static implicit operator RepoResult<T>(RepoConvertible<T> result)
    {
        return result.ToTypedRepoResult();
    }
    
    public static implicit operator RepoResult(RepoConvertible<T> result)
    {
        return result.ToRepoResult();
    }
}
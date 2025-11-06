using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Enums;

namespace Core.Results.Advanced.Abstract;

public abstract class InfraConvertable : RepoConvertable, IInfraConvertable
{
    protected InfraConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected InfraConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected InfraConvertable(IInfraConvertable result) : base(result)
    {
    }
    
    protected InfraConvertable()
    {
    }
    
    public InfraResult<T> ToTypedInfraResult<T>()
    {
        return InfraResult<T>.From(this);
    }
   
    public InfraResult ToInfraResult()
    {
        return InfraResult.From(this);
    }
    
    public static implicit operator InfraResult(InfraConvertable result)
    {
        return result.ToInfraResult();
    }
}

public abstract class InfraConvertable<T> : RepoConvertable<T>, IInfraConvertable<T>
{
    protected InfraConvertable(T value) : base(value)
    {
    }

    protected InfraConvertable(IInfraConvertable valueResult) : base(valueResult)
    {
    }

    protected InfraConvertable(IInfraConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected InfraConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected InfraConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    public InfraResult<T2> ToTypedInfraResult<T2>()
    {
        return InfraResult<T2>.From(this);
    }

    public InfraResult<T> ToTypedInfraResult()
    {
        return InfraResult<T>.From(this);
    }
    
    public InfraResult ToInfraResult()
    {
        return InfraResult.From((IRepoConvertable) this);
    }

    public static implicit operator InfraResult<T>(InfraConvertable<T> result)
    {
        return result.ToTypedInfraResult();
    }
    
    public static implicit operator InfraResult(InfraConvertable<T> result)
    {
        return result.ToInfraResult();
    }
}
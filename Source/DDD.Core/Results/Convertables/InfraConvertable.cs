using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class InfraConvertable : RepoConvertable, IInfraConvertable
{
    protected InfraConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected InfraConvertable(IInfraConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected InfraConvertable(ResultLayer resultLayer)
        : base(resultLayer)
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
    protected InfraConvertable(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected InfraConvertable(IInfraConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected InfraConvertable(IInfraConvertable<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected InfraConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
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
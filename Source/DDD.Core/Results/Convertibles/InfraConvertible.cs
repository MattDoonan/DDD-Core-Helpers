using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class InfraConvertible : RepoConvertible, IInfraConvertible
{
    protected InfraConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected InfraConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected InfraConvertible(ResultLayer resultLayer)
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
    
    public static implicit operator InfraResult(InfraConvertible result)
    {
        return result.ToInfraResult();
    }
}

public abstract class InfraConvertible<T> : RepoConvertible<T>, IInfraConvertible<T>
{
    protected InfraConvertible(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected InfraConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected InfraConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected InfraConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
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
        return InfraResult.From((IRepoConvertible) this);
    }

    public static implicit operator InfraResult<T>(InfraConvertible<T> result)
    {
        return result.ToTypedInfraResult();
    }
    
    public static implicit operator InfraResult(InfraConvertible<T> result)
    {
        return result.ToInfraResult();
    }
}
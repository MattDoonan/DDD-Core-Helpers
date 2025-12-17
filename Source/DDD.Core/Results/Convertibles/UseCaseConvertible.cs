using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class UseCaseConvertible : ResultConvertible, IUseCaseConvertible
{
    
    protected UseCaseConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected UseCaseConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected UseCaseConvertible(ResultLayer layer) 
        : base(layer)
    {
    }
    
    public UseCaseResult ToUseCaseResult()
    {
        return UseCaseResult.From(this);
    }
    
    public UseCaseResult<T> ToTypedUseCaseResult<T>()
    {
        return UseCaseResult<T>.From(this);
    }
    
    public static implicit operator UseCaseResult(UseCaseConvertible result)
    {
        return result.ToUseCaseResult();
    }
}

public abstract class UseCaseConvertible<T> : ResultConvertible<T>, IUseCaseConvertible<T>
{
    protected UseCaseConvertible(T value, ResultLayer resultLayer) : base(value, resultLayer)
    {
    }

    protected UseCaseConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected UseCaseConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected UseCaseConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }
    
    public UseCaseResult<T2> ToTypedUseCaseResult<T2>()
    {
        return UseCaseResult<T2>.From(this);
    }

    public UseCaseResult<T> ToTypedUseCaseResult()
    {
        return UseCaseResult<T>.From(this);
    }

    public UseCaseResult ToUseCaseResult()
    {
        return UseCaseResult.From((IResultConvertible)this);
    }
    
    public static implicit operator UseCaseResult<T>(UseCaseConvertible<T> result)
    {
        return result.ToTypedUseCaseResult();
    }
    
    public static implicit operator UseCaseResult(UseCaseConvertible<T> result)
    {
        return result.ToUseCaseResult();
    }
}
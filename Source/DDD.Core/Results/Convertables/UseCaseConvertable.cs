using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class UseCaseConvertable : ResultConvertable, IUseCaseConvertable
{
    
    protected UseCaseConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected UseCaseConvertable(ResultLayer layer) 
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
    
    public static implicit operator UseCaseResult(UseCaseConvertable result)
    {
        return result.ToUseCaseResult();
    }
}

public abstract class UseCaseConvertable<T> : ResultConvertable<T>, IUseCaseConvertable<T>
{
    protected UseCaseConvertable(T value, ResultLayer resultLayer) : base(value, resultLayer)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected UseCaseConvertable(IUseCaseConvertable<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected UseCaseConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
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
        return UseCaseResult.From((IResultConvertable)this);
    }
    
    public static implicit operator UseCaseResult<T>(UseCaseConvertable<T> result)
    {
        return result.ToTypedUseCaseResult();
    }
    
    public static implicit operator UseCaseResult(UseCaseConvertable<T> result)
    {
        return result.ToUseCaseResult();
    }
}
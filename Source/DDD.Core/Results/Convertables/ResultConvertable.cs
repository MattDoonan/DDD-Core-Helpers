using DDD.Core.Results.Base;
using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Convertables.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertables;

public abstract class ResultConvertable : NonTypedResult, IResultConvertable
{
    protected ResultConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected ResultConvertable(IResultConvertable result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected ResultConvertable(ResultLayer resultLayer) 
        : base(resultLayer) 
    {
    }

    public Result ToResult()
    {
        return Result.From(this);
    }
    
    public Result<T> ToTypedResult<T>()
    {
        return Result<T>.From(this);
    }
    
    public static implicit operator Result(ResultConvertable result)
    {
        return result.ToResult();
    }
}


public abstract class ResultConvertable<T> : TypedResult<T>, IResultConvertable<T>
{
    protected ResultConvertable(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected ResultConvertable(IResultConvertable valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ResultConvertable(IResultConvertable<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ResultConvertable(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }
    
    public Result<T2> ToTypedResult<T2>()
    {
        return Result<T2>.From(this);
    }
    
    public Result<T> ToTypedResult()
    {
        return Result<T>.From(this);
    }

    public Result ToResult()
    {
        return Result.From((IResultStatus)this);
    }
    
    public static implicit operator Result<T>(ResultConvertable<T> result)
    {
        return result.ToTypedResult();
    }

    public static implicit operator Result(ResultConvertable<T> result)
    {
        return result.ToResult();
    }
}
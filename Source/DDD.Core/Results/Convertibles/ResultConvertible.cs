using DDD.Core.Results.Abstract;
using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Convertibles;

public abstract class ResultConvertible : UntypedResult, IResultConvertible
{
    protected ResultConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
        : base(failureType, failedLayer, because)
    {
    }

    protected ResultConvertible(IResultStatus result, ResultLayer? newResultLayer = null) 
        : base(result, newResultLayer)
    {
    }
    
    protected ResultConvertible(ResultLayer resultLayer) 
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
    
    public static implicit operator Result(ResultConvertible result)
    {
        return result.ToResult();
    }
}


public abstract class ResultConvertible<T> : TypedResult<T>, IResultConvertible<T>
{
    protected ResultConvertible(T value, ResultLayer resultLayer) 
        : base(value, resultLayer)
    {
    }

    protected ResultConvertible(IResultStatus valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ResultConvertible(ITypedResult<T> valueResult, ResultLayer? newResultLayer = null) 
        : base(valueResult, newResultLayer)
    {
    }

    protected ResultConvertible(FailureType failureType, ResultLayer failedLayer, string? because) 
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
    
    public static implicit operator Result<T>(ResultConvertible<T> result)
    {
        return result.ToTypedResult();
    }

    public static implicit operator Result(ResultConvertible<T> result)
    {
        return result.ToResult();
    }
}
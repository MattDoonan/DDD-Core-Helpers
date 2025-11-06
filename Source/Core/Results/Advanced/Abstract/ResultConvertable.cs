using Core.Results.Advanced.Interfaces;
using Core.Results.Base.Abstract;
using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;

namespace Core.Results.Advanced.Abstract;

public abstract class ResultConvertable : NonTypedResult, IResultConvertable
{
    protected ResultConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected ResultConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }

    protected ResultConvertable(IResultConvertable result) : base(result)
    {
    }
    
    protected ResultConvertable()
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
    protected ResultConvertable(T value) : base(value)
    {
    }

    protected ResultConvertable(IResultConvertable valueResult) : base(valueResult)
    {
    }

    protected ResultConvertable(IResultConvertable<T> valueResult) : base(valueResult)
    {
    }

    protected ResultConvertable(FailureType failureType, string because) : base(failureType, because)
    {
    }

    protected ResultConvertable(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
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
using System.Runtime.CompilerServices;
using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class BasicValueResult<T, TResult> : ContentResult<T>
    where TResult : BasicResult<TResult>, IResultStatusBase<TResult>
{
    protected BasicValueResult(T value) : base(value)
    {
    }

    protected BasicValueResult(IContentResult<T> valueResult) : base(valueResult)
    {
    }

    protected BasicValueResult(IResultStatus valueResult) : base(valueResult)
    {
    }

    protected BasicValueResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    public TResult RemoveValue()
    {
        return TResult.RemoveValue(this);
    }
    
    public Result<T> ToResult()
    {
        return this;
    }
    
    public static implicit operator TResult(BasicValueResult<T, TResult> result)
    {
        return result.RemoveValue();
    }
    
    public static implicit operator Result<T>(BasicValueResult<T, TResult> result)
    {
        return Result<T>.Create(result);
    }
    
}
using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class BasicContentResultBase<T, TResult> : ContentResult<T>
    where TResult : BasicResult<TResult>, IResultStatusBase<TResult>
{
    protected BasicContentResultBase(T value) : base(value)
    {
    }

    protected BasicContentResultBase(IContentResult<T> valueResult) : base(valueResult)
    {
    }

    protected BasicContentResultBase(IResultStatus valueResult) : base(valueResult)
    {
    }

    protected BasicContentResultBase(FailureType failureType, string because) : base(failureType, because)
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
    
    public static implicit operator TResult(BasicContentResultBase<T, TResult> result)
    {
        return result.RemoveValue();
    }
    
    public static implicit operator Result<T>(BasicContentResultBase<T, TResult> result)
    {
        return Result.Create(result);
    }
    
    public static implicit operator Result(BasicContentResultBase<T, TResult> result)
    {
        return Result.Create(result);
    }
    
    
}
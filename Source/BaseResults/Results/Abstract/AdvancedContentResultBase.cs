using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class AdvancedContentResultBase<T, TResult> : ContentResult<T>
    where TResult : BasicResult<TResult>, IResultStatusBase<TResult>
{
    protected AdvancedContentResultBase(T value) : base(value)
    {
    }

    protected AdvancedContentResultBase(IContentResult<T> valueResult) : base(valueResult)
    {
    }

    protected AdvancedContentResultBase(IResultStatus valueResult) : base(valueResult)
    {
    }

    protected AdvancedContentResultBase(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    public TResult RemoveValue()
    {
        return TResult.RemoveValue(this);
    }
    
    public static implicit operator TResult(AdvancedContentResultBase<T, TResult> result)
    {
        return result.RemoveValue();
    }
}
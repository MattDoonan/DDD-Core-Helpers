using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Base.Abstract;

public abstract class CoreResult<TStatusResult> : ResultStatus
    where TStatusResult : CoreResult<TStatusResult>, IResultFactory<TStatusResult>
{
    protected CoreResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage(), because)
    {
    }
    
    protected CoreResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, failureType.ToMessage(), because)
    {
    }

    protected CoreResult()
    {
    }

    protected CoreResult(IResultStatus result) : base(result)
    {
    }
    
    public static implicit operator CoreResult<TStatusResult>(bool isSuccessful)
    {
        return isSuccessful ? TStatusResult.Pass() : TStatusResult.Fail();;
    }
    
    public static TStatusResult Merge(params IResultStatus[] results)
    {
        var allSuccessful = results.All(r => r.IsSuccessful);
        return allSuccessful
            ? CreateResult(TStatusResult.Pass(), results)
            : CreateResult(TStatusResult.Fail($"Not all {nameof(TStatusResult)} were successful"), results);
    }
    
    private static TStatusResult CreateResult(TStatusResult result, IResultStatus[] results)
    {
        result.ErrorMessages.AddRange(results.SelectMany(r => r.ErrorMessages));
        return result;
    }
}

public abstract class CoreResult<T, TResult> : TypedResult<T>
    where TResult : CoreResult<TResult>, IResultFactory<TResult>
{
    protected CoreResult(T value) : base(value)
    {
    }

    protected CoreResult(ITypedResult<T> valueResult) : base(valueResult)
    {
    }

    protected CoreResult(IResultStatus valueResult) : base(valueResult)
    {
    }

    protected CoreResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected CoreResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    
    public TResult RemoveValue()
    {
        return TResult.RemoveValue(this);
    }
    
    public static implicit operator TResult(CoreResult<T, TResult> result)
    {
        return result.RemoveValue();
    }
}
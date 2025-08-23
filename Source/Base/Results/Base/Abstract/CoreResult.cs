using Outputs.Results.Advanced;
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
    
    public Result ToResult()
    {
        return Result.Create(this);
    }
    
    public TStatusResult Copy()
    {
        return CopyResultValues(this);
    }
    
    public static TStatusResult CreateCopy(TStatusResult result)
    {
        return CopyResultValues(result);
    }

    public static TStatusResult RemoveValue<T>(CoreResult<T, TStatusResult> typedResult)
    {
        return CopyResultValues(typedResult);
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
        result.Errors.AddRange(results.SelectMany(r => r.ErrorMessages));
        return result;
    }

    private static TStatusResult CopyResultValues(IResultStatus result)
    {
        if (result.IsSuccessful)
        {
            return TStatusResult.Pass();
        }
        var newResult = TStatusResult.Fail();
        newResult.Errors.Clear();
        newResult.Errors.AddRange(result.ErrorMessages);
        newResult.FailedLayer  = result.FailedLayer;
        newResult.FailureType = result.FailureType;
        return newResult;
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

    protected CoreResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    protected CoreResult(FailureType failureType, FailedLayer failedLayer, string because) : base(failureType, failedLayer, because)
    {
    }
    
    public Result<T> ToTypedResult()
    {
        return Result.Create(this);
    }
    
    public Result ToResult()
    {
        return Result.Create(this);
    }
    
    public TResult RemoveValue()
    {
        return CoreResult<TResult>.RemoveValue(this);
    }
    
    public static implicit operator TResult(CoreResult<T, TResult> result)
    {
        return result.RemoveValue();
    }
}
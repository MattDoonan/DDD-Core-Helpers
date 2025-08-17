using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class BasicResult<TStatusResult> : ResultStatus
    where TStatusResult : BasicResult<TStatusResult>, IResultStatusBase<TStatusResult>
{
    protected BasicResult(FailureType failureType, string because) : base(failureType, failureType.ToMessage(), because)
    {
    }

    protected BasicResult()
    {
    }

    protected BasicResult(IResultStatus result) : base(result)
    {
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
    
    public static implicit operator Result(BasicResult<TStatusResult> result)
    {
        return Result.Create(result);
    }
    
    public static implicit operator BasicResult<TStatusResult>(bool pass)
    {
        return pass ? TStatusResult.Pass() : TStatusResult.Fail();;
    }
    
}
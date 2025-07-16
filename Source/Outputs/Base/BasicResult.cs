using Outputs.Base.Interfaces;

namespace Outputs.Base;

public abstract class BasicResult<TStatusResult> : ResultStatus
    where TStatusResult : IResultStatusBase<TStatusResult>, IResultStatus
{
    protected BasicResult(string failureMessageStarter, string because) : base(failureMessageStarter, because)
    {
    }

    protected BasicResult(string successLog) : base(successLog)
    {
    }

    protected BasicResult(IResultStatus result) : base(result)
    {
    }

    public static TStatusResult Merge(params IResultStatus[] results)
    {
        var allSuccessful = results.All(r => r.IsSuccessful);
        return allSuccessful
            ? CreateResult(TStatusResult.Pass($"All {nameof(TStatusResult)} were successful"), results)
            : CreateResult(TStatusResult.Fail($"Not all {nameof(TStatusResult)} were successful"), results);
    }
    
    private static TStatusResult CreateResult(TStatusResult result, IResultStatus[] results)
    {
        result.SuccessLogs.AddRange(results.SelectMany(r => r.SuccessLogs));
        result.ErrorMessages.AddRange(results.SelectMany(r => r.ErrorMessages));
        return result;
    }
}
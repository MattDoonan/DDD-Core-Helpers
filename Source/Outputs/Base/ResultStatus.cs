using Outputs.Base.Interfaces;
using Outputs.Helpers;

namespace Outputs.Base;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public List<string> SuccessLogs { get; } = [];
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; } = [];


    protected ResultStatus(string failureMessageStarter, string because) : this(true, failureMessageStarter: failureMessageStarter, because: because)
    {
        
    }
    
    protected ResultStatus(string successLog) : this(false, successLog: successLog)
    {
        
    }
    
    protected ResultStatus(IResultStatus result)
    {
        IsFailure = result.IsFailure;
        SuccessLogs.AddRange(result.SuccessLogs);
        ErrorMessages.AddRange(result.ErrorMessages);
    }
    
    private ResultStatus(
        bool hasFailed, 
        string successLog = "", 
        string failureMessageStarter = "", 
        string because = ""
    )
    {
        IsFailure = hasFailed;
        if (!hasFailed)
        {
            if (!string.IsNullOrWhiteSpace(successLog))
            {
                SuccessLogs.Add(successLog);
            }
            return;
        }
        if (ResultErrorMessage.Create(failureMessageStarter, because, out var errorMessage))
        {
            ErrorMessages.Add(errorMessage);
        }
    }

    protected static TResult AllPass<TResult>(params IResultStatus[] results)
        where TResult : IResultStatusBase<TResult>, IResultStatus
    {
        var allSuccessful = results.All(r => r.IsSuccessful);
        return allSuccessful
            ? CreateResult(TResult.Pass($"All {nameof(TResult)} were successful"), results)
            : CreateResult(TResult.Fail($"Not all {nameof(TResult)} were successful"), results);
    }

    private static TResult CreateResult<TResult>(TResult result, IResultStatus[] results)
        where TResult : IResultStatusBase<TResult>, IResultStatus
    {
        result.SuccessLogs.AddRange(results.SelectMany(r => r.SuccessLogs));
        result.ErrorMessages.AddRange(results.SelectMany(r => r.ErrorMessages));
        return result;
    }
}

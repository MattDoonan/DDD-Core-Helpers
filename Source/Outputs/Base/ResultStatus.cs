using Outputs.Base.Interfaces;
using Outputs.Helpers;

namespace Outputs.Base;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public string SuccessLog { get; } = string.Empty;
    public bool IsFailure { get; }
    public string ErrorReason { get; } = string.Empty;
    public string ErrorMessage
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_baseFailureMessage))
            {
                return string.Empty;
            }
            return string.IsNullOrWhiteSpace(ErrorReason) 
                ? _baseFailureMessage 
                : ResultErrorMessage.Create(_baseFailureMessage, ErrorReason);
        }
    }

    private readonly string _baseFailureMessage;
    
    protected ResultStatus(string failureMessageStarter, string because) : this(true, failureMessageStarter: failureMessageStarter, because: because)
    {
        
    }
    
    protected ResultStatus(string successLog) : this(false, successLog: successLog)
    {
        
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
            SuccessLog = successLog;
            return;
        }
        _baseFailureMessage = failureMessageStarter;
        ErrorReason = because;
    }

    protected static bool AllSucceeded(params IResultStatus[] results)
    {
        return results.All(r => r.IsSuccessful);
    }
}

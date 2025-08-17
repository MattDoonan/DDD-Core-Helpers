using Outputs.Helpers;
using Outputs.Results.Interfaces;

namespace Outputs.Results.Abstract;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; } = [];
    public FailureType FailureType { get; }
    public string MainError => FailureType.ToMessage();

    protected ResultStatus(FailureType failureType, string failureMessageStarter, string because) 
        : this(true, failureType : failureType, failureMessageStarter: failureMessageStarter, because: because)
    {
        
    }
    
    protected ResultStatus() : this(false)
    {
        
    }
    
    protected ResultStatus(IResultStatus result)
    {
        IsFailure = result.IsFailure;
        ErrorMessages.AddRange(result.ErrorMessages);
    }
    
    private ResultStatus(
        bool hasFailed, 
        FailureType failureType = FailureType.None,
        string failureMessageStarter = "", 
        string because = ""
    )
    {
        IsFailure = hasFailed;
        if (!hasFailed)
        {
            return;
        }
        FailureType = failureType;
        if (ResultErrorMessage.Create(failureMessageStarter, because, out var errorMessage))
        {
            ErrorMessages.Add(errorMessage);
        }
    }
    
    public void LogMessage()
    {
        Console.WriteLine(GetErrorMessages());
    }

    public string GetErrorMessages()
    {
        return string.Join(Environment.NewLine, ErrorMessages.ToArray());
    }
}

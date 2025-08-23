using Outputs.Helpers;
using Outputs.Results.Base.Enums;
using Outputs.Results.Base.Interfaces;

namespace Outputs.Results.Base.Abstract;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure { get; }
    public List<string> ErrorMessages { get; } = [];
    public FailureType FailureType { get; }
    public FailedLayer FailedLayer { get; }
    public string MainError => this.MainErrorMessage();

    protected ResultStatus(FailureType failureType, string failureMessageStarter, string because) 
        : this(true, failureType : failureType, failureLayer: FailedLayer.Unknown, failureMessageStarter: failureMessageStarter, because: because)
    {
        
    }
    
    protected ResultStatus(FailureType failureType, FailedLayer failedLayer, string failureMessageStarter, string because) 
        : this(true, failureType : failureType, failureLayer: failedLayer, failureMessageStarter: failureMessageStarter, because: because)
    {
        
    }
    
    protected ResultStatus() : this(false)
    {
        
    }
    
    protected ResultStatus(IResultStatus result)
    {
        FailureType = result.FailureType;
        FailedLayer = result.FailedLayer;
        IsFailure = result.IsFailure;
        ErrorMessages.AddRange(result.ErrorMessages);
    }
    
    private ResultStatus(
        bool hasFailed, 
        FailureType failureType = FailureType.None,
        FailedLayer failureLayer = FailedLayer.None,
        string failureMessageStarter = "", 
        string because = ""
    )
    {
        IsFailure = hasFailed;
        FailureType = failureType;
        FailedLayer = failureLayer;
        if (!hasFailed)
        {
            return;
        }
        if (ResultErrorMessage.TryCreate(failureMessageStarter, because, out var errorMessage))
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

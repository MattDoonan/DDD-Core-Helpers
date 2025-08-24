using Core.Results.Base.Enums;
using Core.Results.Base.Interfaces;
using Core.Results.Helpers;

namespace Core.Results.Base.Abstract;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure { get; }
    
    protected List<string> Errors { get; } = [];

    public IReadOnlyList<string> ErrorMessages => Errors.AsReadOnly();

    public FailureType FailureType { get; protected set; }
    public FailedLayer FailedLayer { get; protected set; }
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
        Errors.AddRange(result.ErrorMessages);
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
            Errors.Add(errorMessage);
        }
        if (failureType is FailureType.None)
        {
            throw new ArgumentException("The failure type cannot be None for a failed result");
        }
        if (failureLayer is FailedLayer.None)
        {
            throw new ArgumentException("The failed layer cannot be None for a failed result");
        }
    }
    
    public bool OperationTimedOut => FailureType is FailureType.OperationTimeout;
    public bool IsAnInvalidRequest => FailureType is FailureType.InvalidRequest;
    public bool IsADomainViolation => FailureType is FailureType.DomainViolation;
    public bool IsNotAllowed => FailureType is FailureType.NotAllowed;
    public bool IsValueObjectFailure => FailureType is FailureType.ValueObject;
    public bool IsMapperFailure => FailureType is FailureType.Mapper;
    public bool IsEntityFailure => FailureType is FailureType.Entity;
    public bool IsNotFound => FailureType is FailureType.NotFound;
    public bool DoesAlreadyExists => FailureType is FailureType.AlreadyExists;
    
    public bool IsFailureType(FailureType failureType)
    {
        return FailureType == failureType;
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

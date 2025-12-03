using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Enums;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Helpers;

namespace DDD.Core.Results.Base;

public abstract class ResultStatus : IResultStatus
{
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure { get; }
    public IReadOnlyCollection<string> ErrorMessages => _errors.AsReadOnly();
    public FailureType FailureType { get; }
    public FailedLayer FailedLayer { get; protected init; }
    public string MainError => this.MainErrorMessage();
    
    private readonly List<string> _errors = [];

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
        _errors.AddRange(result.ErrorMessages);
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
            _errors.Add(errorMessage);
        }
        if (failureType is FailureType.None)
        {
            throw new ResultException("The failure type cannot be None for a failed result.");
        }
        if (failureLayer is FailedLayer.None)
        {
            throw new ResultException("The failed layer cannot be None for a failed result.");
        }
    }
    
    public bool OperationTimedOut => FailureType is FailureType.OperationTimeout;
    public bool IsAnInvalidRequest => FailureType is FailureType.InvalidRequest;
    public bool IsInvalidInput => FailureType is FailureType.InvalidInput;
    public bool IsADomainViolation => FailureType is FailureType.DomainViolation;
    public bool IsNotAllowed => FailureType is FailureType.NotAllowed;
    public bool IsNotFound => FailureType is FailureType.NotFound;
    public bool DoesAlreadyExists => FailureType is FailureType.AlreadyExists;
    public bool IsInvariantViolation => FailureType is FailureType.InvariantViolation;
    public bool IsConcurrencyViolation => FailureType is FailureType.ConcurrencyViolation;

    public bool IsFailureType(FailureType failureType)
    {
        return FailureType == failureType;
    }
    
    public bool IsFailedLayer(FailedLayer failedLayer)
    {
        return FailedLayer == failedLayer;
    }
    
    public void LogMessage()
    {
        Console.WriteLine(ErrorMessagesToString());
    }

    public string ErrorMessagesToString()
    {
        return string.Join(Environment.NewLine, ErrorMessages.ToArray());
    }
    
    public void AddErrorMessage(params string[] messages)
    {
        _errors.AddRange(messages);
    }
}

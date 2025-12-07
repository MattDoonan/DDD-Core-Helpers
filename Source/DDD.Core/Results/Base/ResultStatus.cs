using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Helpers;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Base;

public abstract class ResultStatus : IResultStatus
{
    public FailureType CurrentFailureType { get; private set; }
    public ResultLayer CurrentLayer { get; private set; }
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure => CurrentFailureType is not FailureType.None;
    public IEnumerable<string> ErrorMessages => _errors.ToErrorMessages();
    public IReadOnlyCollection<ResultError> Errors => _errors.AsReadOnly();
    public string MainError => $"{CurrentFailureType.ToMessage()} on the {CurrentLayer.ToMessage()}";
    
    private readonly List<ResultError> _errors = [];
    
    protected ResultStatus(FailureType failureType, ResultLayer failedLayer, string? because) 
        : this(new ResultError(failureType, failedLayer, because))
    {
    }
    
    protected ResultStatus(ResultLayer layer) 
    {
        CurrentFailureType = FailureType.None;
        CurrentLayer = layer;
    }
    
    protected ResultStatus(IResultStatus result, ResultLayer? newResultLayer = null)
    {
        CurrentFailureType = result.CurrentFailureType;
        CurrentLayer = newResultLayer ?? result.CurrentLayer;
        _errors.AddRange(
            newResultLayer is not null
                ? result.Errors.AddLayer(newResultLayer.Value) 
                : result.Errors);
    }
    
    protected ResultStatus(ResultError error)
    {
        CurrentFailureType = error.FailureType;
        CurrentLayer = error.ResultLayer;
        _errors.Add(error);
    }

    public bool OperationTimedOut => _errors.Contains(FailureType.OperationTimeout);
    public bool IsAnInvalidRequest => _errors.Contains(FailureType.InvalidRequest);
    public bool IsInvalidInput => _errors.Contains(FailureType.InvalidInput);
    public bool IsADomainViolation => _errors.Contains(FailureType.DomainViolation);
    public bool IsNotAllowed => _errors.Contains(FailureType.NotAllowed);
    public bool IsNotFound => _errors.Contains(FailureType.NotFound);
    public bool DoesAlreadyExists => _errors.Contains(FailureType.AlreadyExists);
    public bool IsInvariantViolation => _errors.Contains(FailureType.InvariantViolation);
    public bool IsConcurrencyViolation => _errors.Contains(FailureType.ConcurrencyViolation);

    public bool ContainsFailureType(FailureType failureType)
    {
        if (_errors.Count == 0 && failureType is FailureType.None)
        {
            return true;
        }
        return _errors.Contains(failureType);
    }
    
    public bool IsFromLayer(ResultLayer failedLayer)
    {
        return _errors.Contains(failedLayer);
    }
    
    public void LogMessage()
    {
        Console.WriteLine(ErrorMessagesToString());
    }

    public string ErrorMessagesToString()
    {
        return string.Join(Environment.NewLine, ErrorMessages.ToArray());
    }

    public void CombineWith(params IResultStatus[] resultStatuses)
    {
        foreach (var status in resultStatuses)
        {
            CombineWith(status);
        }
    }

    public void CombineWith(IResultStatus resultStatus)
    {
        if (resultStatus.IsSuccessful && IsSuccessful)
        {
            return;
        }
        if (IsFailure && resultStatus.IsSuccessful)
        {
            return;
        }
        if (IsSuccessful && resultStatus.IsFailure)
        {
            CurrentFailureType = resultStatus.CurrentFailureType;
        }
        _errors.AddRange(resultStatus.Errors.AddLayer(CurrentLayer));
    }

    public void Add(ResultError error)
    {
        _errors.Add(error);
    }
    
    public void AddErrorMessage(params string[] messages)
    {
        if (CurrentFailureType is FailureType.None)
        {
            throw new ResultException(this, "Cannot add an error message to a successful result");
        }
        foreach (var message in messages)
        {
            _errors.AddRange(new ResultError(CurrentFailureType, CurrentLayer, message));
        }
    }
    
    public ResultException ToException()
    {
        return new ResultException(this);
    }
    
    public void Throw()
    {
        throw new ResultException(this);
    }
}

using DDD.Core.Results.Base.Interfaces;
using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Base;

public abstract class ResultStatus : IResultStatus
{
    public FailureType PrimaryFailureType { get; private set; }
    public ResultLayer CurrentLayer { get; private set; }
    public bool IsSuccessful => !IsFailure;
    public bool IsFailure => PrimaryFailureType is not FailureType.None;
    public IEnumerable<string> ErrorMessages => _errors.ToErrorMessages();
    public IReadOnlyCollection<ResultError> Errors => _errors.AsReadOnly();
    public string MainError => $"{PrimaryFailureType.ToMessage()} on the {CurrentLayer.ToMessage()}";
    
    private readonly List<ResultError> _errors = [];
    
    protected ResultStatus(FailureType failureType, ResultLayer failedLayer, string? because) 
        : this(new ResultError(failureType, failedLayer, because))
    {
    }
    
    protected ResultStatus(ResultLayer layer) 
    {
        PrimaryFailureType = FailureType.None;
        CurrentLayer = layer;
    }
    
    protected ResultStatus(IResultStatus result, ResultLayer? newResultLayer = null)
    {
        PrimaryFailureType = result.PrimaryFailureType;
        CurrentLayer = newResultLayer ?? result.CurrentLayer;
        _errors.AddRange(
            newResultLayer is not null
                ? result.Errors.AddLayer(newResultLayer.Value) 
                : result.Errors);
    }
    
    protected ResultStatus(ResultError error)
    {
        PrimaryFailureType = error.FailureType;
        CurrentLayer = error.ResultLayer;
        _errors.Add(error);
    }

    public bool OperationTimedOut => ContainsFailureType(FailureType.OperationTimeout);
    public bool IsAnInvalidRequest => ContainsFailureType(FailureType.InvalidRequest);
    public bool IsInvalidInput => ContainsFailureType(FailureType.InvalidInput);
    public bool IsADomainViolation => ContainsFailureType(FailureType.DomainViolation);
    public bool IsNotAllowed => ContainsFailureType(FailureType.NotAllowed);
    public bool IsNotFound => ContainsFailureType(FailureType.NotFound);
    public bool DoesAlreadyExists => ContainsFailureType(FailureType.AlreadyExists);
    public bool IsInvariantViolation => ContainsFailureType(FailureType.InvariantViolation);
    public bool IsConcurrencyViolation => ContainsFailureType(FailureType.ConcurrencyViolation);

    public bool IsPrimaryFailure(FailureType expectedFailure)
    {
        return PrimaryFailureType == expectedFailure;
    }
    
    public bool IsFromLayer(ResultLayer failedLayer)
    {
        return _errors.Contains(failedLayer);
    }
    
    public bool ContainsFailureType(FailureType failureType)
    {
        if (_errors.Count == 0 && failureType is FailureType.None)
        {
            return true;
        }
        return _errors.Contains(failureType);
    }

    public void CombineWith(params IResultStatus[] resultStatuses)
    {
        foreach (var status in resultStatuses)
        {
            CombineWith(status);
        }
    }

    public void CombineWith(IResultStatus otherResult)
    {
        if (BothSuccessful(otherResult))
        {
            return;
        }
        if (IsFailure && otherResult.IsSuccessful)
        {
            return;
        }
        if (IsSuccessful && otherResult.IsFailure)
        {
            PrimaryFailureType = otherResult.PrimaryFailureType;
        }
        AddRange(otherResult.Errors);
    }
    
    public void AddRange(IEnumerable<ResultError> errors)
    {
        _errors.AddRange(errors.AddLayer(CurrentLayer));
    }

    public void Add(ResultError error)
    {
        _errors.Add(error.AddLayer(CurrentLayer));
    }
    
    public void AddErrorMessage(params string[] messages)
    {
        if (IsPrimaryFailure(FailureType.None))
        {
            Throw("Cannot add an error message to a successful result");
        }
        foreach (var message in messages)
        {
            Add(new ResultError(PrimaryFailureType, CurrentLayer, message));
        }
    }
    
    public IEnumerable<ResultError> GetErrorsBy(FailureType failureType)
    {
        return GetErrorsBy(e => e.IsFailureType(failureType));
    }

    public IEnumerable<ResultError> GetErrorsBy(ResultLayer layer)
    {
        return GetErrorsBy(e => e.IsLayer(layer));
    }

    public IEnumerable<ResultError> GetErrorsOfType<T>()
    {
        return GetErrorsBy(e => e.IsOfType<T>());
    }

    public IEnumerable<ResultError> GetErrorsBy(Func<ResultError, bool> predicate)
    {
        return _errors.Where(predicate);
    }

    public bool BothSuccessful(IResultStatus otherResult)
    {
        return IsSuccessful && otherResult.IsSuccessful;
    }
    
    public void LogMessage()
    {
        Console.WriteLine(ErrorMessagesToString());
    }

    public string ErrorMessagesToString()
    {
        return string.Join(Environment.NewLine, ErrorMessages.ToArray());
    }
    
    public void Throw(string message)
    {
        throw ToException(message);
    }

    public void Throw()
    {
        throw ToException();
    }

    public ResultException ToException(string message)
    {
        return new ResultException(this);
    }
    
    public ResultException ToException()
    {
        return new ResultException(this);
    }
}

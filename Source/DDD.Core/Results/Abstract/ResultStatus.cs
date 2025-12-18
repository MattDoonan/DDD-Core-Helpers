using DDD.Core.Results.Exceptions;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Abstract;

/// <summary>
/// Abstract base class for result statuses.
/// Represents the status of an operation result, including success or failure information.
/// </summary>
public abstract class ResultStatus : IResultStatus
{
    /// <summary>
    /// The primary failure type of the result.
    /// </summary>
    public FailureType PrimaryFailureType { get; private set; }
    
    /// <summary>
    /// The layer at which the result was generated.
    /// </summary>
    public ResultLayer CurrentLayer { get; private set; }
    
    /// <summary>
    /// Indicates whether the result was successful.
    /// </summary>
    public bool IsSuccessful => !IsFailure;
    
    /// <summary>
    /// Indicates whether the result was a failure.
    /// </summary>
    public bool IsFailure => PrimaryFailureType is not FailureType.None;
    
    /// <summary>
    /// The error messages associated with the result.
    /// </summary>
    public IEnumerable<string> ErrorMessages => _errors.ToErrorMessages();
    
    /// <summary>
    /// The collection of errors associated with the result.
    /// </summary>
    public IReadOnlyCollection<ResultError> Errors => _errors.AsReadOnly();
    
    /// <summary>
    /// A summary of the main error in the result.
    /// </summary>
    public string MainError => $"{PrimaryFailureType.ToMessage()} on the {CurrentLayer.ToMessage()}";
    
    private readonly List<ResultError> _errors = [];
    
    /// <summary>
    /// Initializes a new successful instance of the <see cref="ResultStatus"/> class
    /// at the specified layer.
    /// </summary>
    /// <param name="layer">
    /// The result layer where the successful result is generated.
    /// </param>
    protected ResultStatus(ResultLayer layer) 
    {
        PrimaryFailureType = FailureType.None;
        CurrentLayer = layer;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultStatus"/> class
    /// by copying from an existing result status, optionally updating the result layer.
    /// </summary>
    /// <param name="result">
    /// The existing result status to copy from.
    /// </param>
    /// <param name="newResultLayer">
    /// The new result layer to set, if different from the existing one.
    /// </param>
    protected ResultStatus(IResultStatus result, ResultLayer? newResultLayer = null)
    {
        PrimaryFailureType = result.PrimaryFailureType;
        CurrentLayer = newResultLayer ?? result.CurrentLayer;
        _errors.AddRange(newResultLayer is not null
                ? result.Errors.WithLayer(newResultLayer.Value) 
                : result.Errors);
    }
    
    /// <summary>
    /// Initializes a new failure instance of the <see cref="ResultStatus"/> class
    /// with the specified <see cref="ResultError"/>.
    /// </summary>
    /// <param name="error">
    /// The error to initialize the result status with.
    /// </param>
    protected ResultStatus(ResultError error)
    {
        PrimaryFailureType = error.FailureType;
        CurrentLayer = error.ResultLayer;
        _errors.Add(error);
    }
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.OperationTimeout"/> is present.
    /// </summary>
    public bool OperationTimedOut => ContainsErrorWith(FailureType.OperationTimeout);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.InvalidRequest"/> is present.
    /// </summary>
    public bool IsAnInvalidRequest => ContainsErrorWith(FailureType.InvalidRequest);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.InvalidInput"/> is present.
    /// </summary>
    public bool IsInvalidInput => ContainsErrorWith(FailureType.InvalidInput);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.DomainViolation"/> is present.
    /// </summary>
    public bool IsDomainViolation => ContainsErrorWith(FailureType.DomainViolation);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.NotAllowed"/> is present.
    /// </summary>
    public bool IsNotAllowed => ContainsErrorWith(FailureType.NotAllowed);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.NotFound"/> is present.
    /// </summary>
    public bool IsNotFound => ContainsErrorWith(FailureType.NotFound);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.AlreadyExists"/> is present.
    /// </summary>
    public bool AlreadyExisting => ContainsErrorWith(FailureType.AlreadyExists);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.InvariantViolation"/> is present.
    /// </summary>
    public bool IsInvariantViolation => ContainsErrorWith(FailureType.InvariantViolation);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.ConcurrencyViolation"/> is present.
    /// </summary>
    public bool IsConcurrencyViolation => ContainsErrorWith(FailureType.ConcurrencyViolation);
    
    /// <summary>
    /// Indicates whether a saved <see cref="ResultError"/>
    /// with the failure type <see cref="FailureType.OperationCancelled"/> is present.
    /// </summary>
    public bool OperationIsCancelled => ContainsErrorWith(FailureType.OperationCancelled);
    
    /// <summary>
    /// Updates the current result layer.
    /// </summary>
    /// <param name="newlayer">
    /// The new result layer to be set.
    /// </param>
    public void SetCurrentLayer(ResultLayer newlayer) => CurrentLayer = newlayer;
    
    /// <summary>
    /// Updates the primary failure type.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure type to be set.
    /// </param>
    /// <exception cref="ResultException">
    /// Thrown if attempting to set the primary failure type to None
    /// when there are already errors present.
    /// </exception>
    public void SetPrimaryFailure(FailureType newPrimaryFailure)
    {
        ThrowIfTypeIsNoneAndErrorsExist(newPrimaryFailure);
        PrimaryFailureType = newPrimaryFailure;
    }

    /// <summary>
    /// Checks if the primary failure type matches the expected failure type.
    /// </summary>
    /// <param name="expectedFailure">
    /// The expected failure type to check against.
    /// </param>
    /// <returns>
    /// True if the primary failure type matches the expected failure type; otherwise, false.
    /// </returns>
    public bool IsPrimaryFailure(FailureType expectedFailure) => PrimaryFailureType == expectedFailure;
    
    /// <summary>
    /// Checks if the current result layer matches the specified failed layer.
    /// </summary>
    /// <param name="failedLayer">
    /// The failed layer to check for.
    /// </param>
    /// <returns>
    /// True if the current result layer matches the specified failed layer; otherwise, false.
    /// </returns>
    public bool IsCurrentLayer(ResultLayer failedLayer) => CurrentLayer == failedLayer;
    
    /// <summary>
    /// Checks if any error in the result matches the specified failed layer.
    /// </summary>
    /// <param name="failedLayer">
    /// The failed layer to check for.
    /// </param>
    /// <returns>
    /// True if any error matches the specified failed layer; otherwise, false.
    /// </returns>
    public bool ContainsErrorWith(ResultLayer failedLayer) => _errors.Contains(failedLayer);
    
    /// <summary>
    /// Checks if any error in the result matches the specified failure type.
    /// </summary>
    /// <param name="failureType">
    /// The failure type to check for.
    /// </param>
    /// <returns>
    /// True if any error matches the specified failure type; otherwise, false.
    /// </returns>
    public bool ContainsErrorWith(FailureType failureType)
    {
        if (_errors.Count == 0 && failureType is FailureType.None)
            return true;
        return _errors.Contains(failureType);
    }
    
    /// <summary>
    /// Combines the current result status with other result statuses.
    /// Changes the primary failure type to Generic if any of the other results are failures.
    /// </summary>
    /// <param name="otherResults">
    /// The other result statuses to be combined.
    /// </param>
    public void CombineWith(params IResultStatus[] otherResults)
    {
        foreach (var status in otherResults)
            CombineWith(FailureType.Generic, status);
    }

    /// <summary>
    /// Combines the current result status with other result statuses.
    /// Updates the primary failure type if any of the other results are failures.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure type to set if any of the other results are failures.
    /// </param>
    /// <param name="otherResults">
    /// The other result statuses to be combined.
    /// </param>
    public void CombineWith(FailureType newPrimaryFailure, params IResultStatus[] otherResults)
    {
        if (otherResults.AllSuccessful())
            return;
        var errors = otherResults.SelectMany(r => r.Errors).ToArray();
        if (errors.Length == 0)
        {
            SetPrimaryFailure(newPrimaryFailure);
            return;
        }
        AddErrors(newPrimaryFailure, errors);
    }
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status
    /// and sets the primary failure type to Generic.
    /// </summary>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddErrors(IEnumerable<ResultError> errors) => AddError(FailureType.Generic, errors.ToArray());
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure type to set, if any.
    /// </param>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddErrors(FailureType newPrimaryFailure, IEnumerable<ResultError> errors) => AddError(newPrimaryFailure, errors.ToArray());
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status
    /// and sets the primary failure type to Generic.
    /// </summary>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddError(params ResultError[] errors) => AddError(FailureType.Generic, errors);
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status.
    /// Then sets the primary failure type to the specified new primary failure type.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure type to set.
    /// </param>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    /// <exception cref="ResultException">
    /// Thrown if attempting to set the primary failure type to None
    /// when there are already errors present.
    /// </exception>
    public void AddError(FailureType newPrimaryFailure, params ResultError[] errors)
    {
        SetPrimaryFailure(newPrimaryFailure);
        _errors.AddRange(errors.WithLayer(CurrentLayer));
    }

    /// <summary>
    /// Creates a new <see cref="ResultError"/> for each provided message
    /// with the current primary failure type and layer,
    /// and adds them to the result status.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure type to set.
    /// </param>
    /// <param name="messages">
    /// The error messages to be added.
    /// </param>
    public void AddErrorMessage(FailureType newPrimaryFailure, params string[] messages)
    {
        AddErrors(newPrimaryFailure, messages.Select(m => new ResultError(PrimaryFailureType, CurrentLayer, m)));
    }
    
    /// <summary>
    /// Retrieves all errors that match the specified failure type.
    /// </summary>
    /// <param name="failureType">
    /// The failure type to filter errors by.
    /// </param>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/>s that match the specified failure type.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsBy(FailureType failureType) => _errors.GetErrorsBy(failureType);

    /// <summary>
    /// Retrieves all errors that occurred at the specified result layer.
    /// </summary>
    /// <param name="layer">
    /// The result layer to filter errors by.
    /// </param>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/>s that occurred at the specified layer.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsBy(ResultLayer layer) => _errors.GetErrorsBy(layer);

    /// <summary>
    /// Retrieves all errors of the specified type.
    /// </summary>
    /// <typeparam name="TError">
    /// The type inside <see cref="ResultError"/> to filter by.
    /// </typeparam>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/>s with the specified type.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsOfType<TError>() => _errors.GetErrorsOfType<TError>();

    /// <summary>
    /// Retrieves all errors that match the provided predicate.
    /// </summary>
    /// <param name="predicate">
    /// The predicate function to filter errors.
    /// </param>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/>s that match the predicate.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsBy(Func<ResultError, bool> predicate) => _errors.GetErrorsBy(predicate);

    
    /// <summary>
    /// Checks if both the current result and another result are successful.
    /// </summary>
    /// <param name="otherResult">
    /// The other result status to check for success.
    /// </param>
    /// <returns>
    /// True if both results are successful; otherwise, false.
    /// </returns>
    public bool BothSuccessful(IResultStatus otherResult) => IsSuccessful && otherResult.IsSuccessful;
    
    /// <summary>
    /// Logs the error messages to the console.
    /// </summary>
    public void LogMessage() => Console.WriteLine(ErrorMessagesToString());

    
    /// <summary>
    /// Converts the error messages to a single string, separated by new lines.
    /// </summary>
    /// <returns></returns>
    public string ErrorMessagesToString() => string.Join(Environment.NewLine, ErrorMessages);
    
    /// <summary>
    /// Throws a <see cref="ResultException"/> with the specified message.
    /// </summary>
    /// <param name="message">
    /// The message to include in the exception.
    /// </param>
    /// <exception cref="ResultException">
    /// Thrown to indicate the result status as an exception.
    /// </exception>
    public void Throw(string message) => throw ToException(message);

    /// <summary>
    /// Throws a <see cref="ResultException"/> representing the current result status.
    /// </summary>
    /// <exception cref="ResultException">
    /// Thrown to indicate the result status as an exception.
    /// </exception>
    public void Throw() => throw ToException();

    /// <summary>
    /// Converts the current result status to a <see cref="ResultException"/>.
    /// </summary>
    /// <param name="message">
    /// The message to include in the exception.
    /// </param>
    /// <returns>
    /// A <see cref="ResultException"/> representing the current result status.
    /// </returns>
    public ResultException ToException(string message) => new (this, message);
    
    /// <summary>
    /// Converts the current result status to a <see cref="ResultException"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ResultException"/> representing the current result status.
    /// </returns>
    public ResultException ToException() => new (this);
    
    
    private void ThrowIfTypeIsNoneAndErrorsExist(FailureType failureType)
    {
        if (failureType == FailureType.None && 0 < _errors.Count)
            Throw("Cannot set primary failure to 'None' whe the result is already a failure and errors are present.");
    }
}

using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
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
    /// The operation's primary status.
    /// Can be a <see cref="Success"/> or a variety of <see cref="FailedOperationStatus"/>s.
    /// </summary>
    public OperationStatus PrimaryStatus { get; private set; }
    
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
    public bool IsFailure => PrimaryStatus is not Success;
    
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
    public string MainError => $"{PrimaryStatus.Message} on the {CurrentLayer.ToMessage()}";
    
    private readonly List<ResultError> _errors = [];

    /// <summary>
    /// Initializes a new successful instance of the <see cref="ResultStatus"/> class
    /// at the specified layer.
    /// </summary>
    /// <param name="successStatus">
    /// The success status to initialize the result with.
    /// </param>
    /// <param name="layer">
    /// The result layer where the successful result is generated.
    /// </param>
    protected ResultStatus(Success successStatus, ResultLayer layer) 
    {
        PrimaryStatus = successStatus;
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
        PrimaryStatus = result.PrimaryStatus;
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
        PrimaryStatus = error.Failure;
        CurrentLayer = error.ResultLayer;
        _errors.Add(error);
    }
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.OperationTimeout"/> is present.
    /// </summary>
    public bool OperationTimedOut => ContainsErrorWith(StatusType.OperationTimeout);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.InvalidRequest"/> is present.
    /// </summary>
    public bool IsAnInvalidRequest => ContainsErrorWith(StatusType.InvalidRequest);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.InvalidInput"/> is present.
    /// </summary>
    public bool IsInvalidInput => ContainsErrorWith(StatusType.InvalidInput);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.DomainViolation"/> is present.
    /// </summary>
    public bool IsDomainViolation => ContainsErrorWith(StatusType.DomainViolation);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.NotAllowed"/> is present.
    /// </summary>
    public bool IsNotAllowed => ContainsErrorWith(StatusType.NotAllowed);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.NotFound"/> is present.
    /// </summary>
    public bool IsNotFound => ContainsErrorWith(StatusType.NotFound);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.AlreadyExists"/> is present.
    /// </summary>
    public bool AlreadyExisting => ContainsErrorWith(StatusType.AlreadyExists);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.InvariantViolation"/> is present.
    /// </summary>
    public bool IsInvariantViolation => ContainsErrorWith(StatusType.InvariantViolation);
    
    /// <summary>
    /// Indicates whether a <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.ConcurrencyViolation"/> is present.
    /// </summary>
    public bool IsConcurrencyViolation => ContainsErrorWith(StatusType.ConcurrencyViolation);
    
    /// <summary>
    /// Indicates whether a saved <see cref="ResultError"/>
    /// with the failure type <see cref="StatusType.OperationCancelled"/> is present.
    /// </summary>
    public bool OperationIsCancelled => ContainsErrorWith(StatusType.OperationCancelled);
    
    /// <summary>
    /// Updates the current result layer.
    /// </summary>
    /// <param name="newlayer">
    /// The new result layer to be set.
    /// </param>
    public void SetCurrentLayer(ResultLayer newlayer) => CurrentLayer = newlayer;
    
    /// <summary>
    /// Sets the primary status to the new operation status.
    /// Will throw an exception if attempting to set to Success while errors exist.
    /// </summary>
    /// <param name="newStatus">
    /// The new operation status to be set.
    /// </param>
    public void SetPrimaryStatus(OperationStatus newStatus)
    {
        ThrowIfTypeIsNoneAndErrorsExist(newStatus.Type);
        PrimaryStatus = newStatus;
    }

    /// <summary>
    /// Checks if the current primary status matches the specified status.
    /// </summary>
    /// <param name="status">
    /// The <see cref="OperationStatus"/> to check against.
    /// </param>
    /// <returns>
    /// True if the current primary status matches the specified status; otherwise, false.
    /// </returns>
    public bool IsPrimaryStatus(OperationStatus status) => PrimaryStatus == status;
    
    /// <summary>
    /// Checks if the current primary status type matches the specified status type.
    /// </summary>
    /// <param name="statusType">
    /// The <see cref="StatusType"/> to check against.
    /// </param>
    /// <returns>
    /// True if the current primary status type matches the specified status type; otherwise, false.
    /// </returns>
    public bool IsPrimaryStatus(StatusType statusType) => PrimaryStatus.Type == statusType;
    
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
    /// <param name="statusType">
    /// The status type to check for.
    /// </param>
    /// <returns>
    /// True if any error matches the specified failure type; otherwise, false.
    /// </returns>
    public bool ContainsErrorWith(StatusType statusType)
    {
        if (_errors.Count == 0 && statusType is StatusType.Success)
            return true;
        return _errors.Contains(statusType);
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
            CombineWith(OperationStatus.Failure(), status);
    }

    /// <summary>
    /// Combines the current result status with other result statuses.
    /// Updates the primary failure type if any of the other results are failures.
    /// </summary>
    /// <param name="newPrimaryStatus">
    /// The new primary failure operation status to set if any failures are found.
    /// </param>
    /// <param name="otherResults">
    /// The other result statuses to be combined.
    /// </param>
    public void CombineWith(FailedOperationStatus newPrimaryStatus, params IResultStatus[] otherResults)
    {
        if (otherResults.AllSuccessful())
            return;
        var errors = otherResults.SelectMany(r => r.Errors).ToArray();
        if (errors.Length == 0)
        {
            SetPrimaryStatus(newPrimaryStatus);
            return;
        }
        AddErrors(newPrimaryStatus, errors);
    }
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status
    /// and sets the primary failure type to Generic.
    /// </summary>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddErrors(IEnumerable<ResultError> errors) 
        => AddError(OperationStatus.Failure(), errors.ToArray());
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status.
    /// </summary>
    /// <param name="newPrimaryStatus">
    /// The new primary failure type to set, if any.
    /// </param>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddErrors(FailedOperationStatus newPrimaryStatus, IEnumerable<ResultError> errors) 
        => AddError(newPrimaryStatus, errors.ToArray());
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status
    /// and sets the primary failure type to Generic.
    /// </summary>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    public void AddError(params ResultError[] errors) => AddError(OperationStatus.Failure(), errors);
    
    /// <summary>
    /// Adds multiple <see cref="ResultError"/>s to the result status.
    /// Then sets the primary failure type to the specified new primary failure type.
    /// </summary>
    /// <param name="newPrimaryStatus">
    /// The new primary failure status type to set.
    /// </param>
    /// <param name="errors">
    /// The errors to be added.
    /// </param>
    /// <exception cref="ResultException">
    /// Thrown if attempting to set the primary failure type to None
    /// when there are already errors present.
    /// </exception>
    public void AddError(FailedOperationStatus newPrimaryStatus, params ResultError[] errors)
    {
        SetPrimaryStatus(newPrimaryStatus);
        _errors.AddRange(errors.WithLayer(CurrentLayer));
    }

    /// <summary>
    /// Creates a new <see cref="ResultError"/> for each provided message
    /// with the current primary failure type and layer,
    /// and adds them to the result status.
    /// </summary>
    /// <param name="newPrimaryFailure">
    /// The new primary failure status, type to set.
    /// </param>
    /// <param name="messages">
    /// The error messages to be added.
    /// </param>
    public void AddErrorMessage(FailedOperationStatus newPrimaryFailure, params string[] messages)
        => AddErrors(newPrimaryFailure, messages.Select(message => new ResultError(newPrimaryFailure, CurrentLayer, message)));
    
    /// <summary>
    /// Retrieves all errors that match the specified status type.
    /// </summary>
    /// <param name="statusType">
    /// The status type to filter errors by.
    /// </param>
    /// <returns>
    /// An enumerable of <see cref="ResultError"/>s that match the specified status type.
    /// </returns>
    public IEnumerable<ResultError> GetErrorsBy(StatusType statusType) => _errors.GetErrorsBy(statusType);

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
    /// Throws the exception associated with the primary failure status, if it is a failure.
    /// </summary>
    public void ThrowIfFailure()
    {
        if (PrimaryStatus is FailedOperationStatus failedOperationStatus)
        {
            failedOperationStatus.Throw();
        }
    }
    
    
    private void ThrowIfTypeIsNoneAndErrorsExist(StatusType statusType)
    {
        if (statusType == StatusType.Success && 0 < _errors.Count)
            throw new ResultConversionException(this, "Cannot set primary status to Success when there are existing errors.");
    }
    
    
}

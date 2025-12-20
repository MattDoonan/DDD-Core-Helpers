using DDD.Core.Operations.Exceptions;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Operations.Statuses.ValueObjects;
using DDD.Core.Results.Extensions;

namespace DDD.Core.Results.ValueObjects;

/// <summary>
/// A record that represents an error that occurred during a result operation.
/// </summary>
public record ResultError
{
    /// <summary>
    /// The type of failure that occurred.
    /// </summary>
    public readonly FailedOperationStatus Failure;
    
    /// <summary>
    /// The layer in which the error occurred.
    /// </summary>
    public readonly ResultLayer ResultLayer;
    
    /// <summary>
    /// An optional message explaining why the error occurred. Can be null.
    /// If provided, it is appended to the error message as: "because {Because}".
    /// </summary>
    public readonly string? Because;
    
    /// <summary>
    /// The type of the output that was expected when the error occurred. Can be null.
    /// </summary>
    public readonly Type? OutputType;
    
    public ResultError(FailedOperationStatus failedOperationStatus, ResultLayer resultLayer) 
        : this(failedOperationStatus, resultLayer, null)
    {
    }
    
    public ResultError(FailedOperationStatus failedOperationStatus, ResultLayer resultLayer, string? because = null)
    {
        Failure = failedOperationStatus;
        ResultLayer = resultLayer;
        Because = because;
        OutputType = failedOperationStatus.OutputType;
    }
    
    /// <summary>
    /// Checks if the OutputType is of type T.
    /// </summary>
    /// <typeparam name="T">
    /// The type to check against.
    /// </typeparam>
    /// <returns>
    /// True if OutputType is of type T, false otherwise.
    /// </returns>
    public bool IsOfType<T>() 
        => IsOfType(typeof(T));
    
    /// <summary>
    /// Checks if the OutputType is of the given type.
    /// </summary>
    /// <param name="type">
    /// The type to check against.
    /// </param>
    /// <returns>
    /// True if OutputType is of the given type, false otherwise.
    /// </returns>
    public bool IsOfType(Type type) 
        => OutputType == type;
   
    /// <summary>
    /// Checks if the failure type matches the given failure type.
    /// </summary>
    /// <param name="expectedFailureType">
    /// The expected failure type to check against.
    /// </param>
    /// <returns>
    /// True if the failure type matches, false otherwise.
    /// </returns>
    public bool IsFailureType(FailedOperationStatus expectedFailureType) 
        => Failure.Type == expectedFailureType.Type;
    
    /// <summary>
    /// Checks if the failure type matches the given status type.
    /// </summary>
    /// <param name="expectedStatus">
    /// The expected status type to check against.
    /// </param>
    /// <returns></returns>
    public bool IsFailureType(StatusType expectedStatus) 
        => Failure.Type == expectedStatus;

    /// <summary>
    /// Checks if the <see cref="ResultLayer"/> matches the given layer.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public bool IsLayer(ResultLayer layer) 
        => ResultLayer == layer;

    /// <summary>
    /// Converts the error to a human-readable error message.
    /// Optionally includes the reason if provided.
    /// </summary>
    /// <returns></returns>
    public string ToErrorMessage()
    {
        var sentenceStarter = $"{Failure.Message} on the {ResultLayer.ToMessage()}";
        return string.IsNullOrWhiteSpace(Because) 
            ? sentenceStarter 
            : $"{sentenceStarter} because {Because}";
    }

    /// <summary>
    /// Returns a new <see cref="ResultError"/> with the specified layer if the current layer is Unknown.
    /// </summary>
    /// <param name="layer">
    /// The new layer to set.
    /// </param>
    /// <returns>
    /// A new <see cref="ResultError"/> with the specified layer,
    /// or the current instance if the layer is not Unknown.
    /// </returns>
    public ResultError WithLayer(ResultLayer layer) 
        => ResultLayer is ResultLayer.Unknown 
            ? new ResultError(Failure, layer, Because) 
            : this;
    
    
    /// <summary>
    /// Throws an <see cref="OperationException"/> based on the failure type.
    /// </summary>
    /// <exception cref="OperationException">
    /// Thrown to indicate the operation failure.
    /// </exception>
    public void Throw() => throw GetFailureException();

    /// <summary>
    /// Converts the failure to an <see cref="OperationException"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="OperationException"/> representing the failure.
    /// </returns>
    public OperationException GetFailureException() => Failure.ToException();
}

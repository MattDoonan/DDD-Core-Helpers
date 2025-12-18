using DDD.Core.Results.Exceptions;
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
    public readonly FailureType FailureType;
    
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

    public ResultError(FailureType failureType, ResultLayer resultLayer, string? because)
        : this(failureType, resultLayer, because, null)
    {
    }
    
    public ResultError(FailureType failureType, ResultLayer resultLayer, Type outputType) 
        : this(failureType, resultLayer, null, outputType)
    {
    }
    
    public ResultError(FailureType failureType, ResultLayer resultLayer, string? because = null, Type? outputType = null)
    {
        if (failureType is FailureType.None)
        {
            throw new ArgumentException("An error message cannot contain none as a failure type.");
        }
        FailureType = failureType;
        ResultLayer = resultLayer;
        Because = because;
        OutputType = outputType;
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
    {
        return IsOfType(typeof(T));
    }
    
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
    {
        return OutputType == type;
    }
    
    /// <summary>
    /// Checks if the FailureType matches the given failure type.
    /// </summary>
    /// <param name="failureType">
    /// The failure type to check against.
    /// </param>
    /// <returns>
    /// True if the <see cref="FailureType"/> matches the given failure type, false otherwise.
    /// </returns>
    public bool IsFailureType(FailureType failureType)
    {
        return FailureType == failureType;
    }

    /// <summary>
    /// Checks if the <see cref="ResultLayer"/> matches the given layer.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public bool IsLayer(ResultLayer layer)
    {
        return ResultLayer == layer;
    }

    /// <summary>
    /// Converts the error to a human-readable error message.
    /// Optionally includes the reason if provided.
    /// </summary>
    /// <returns></returns>
    public string ToErrorMessage()
    {
        var sentenceStarter = $"{FailureType.ToMessage(OutputType)} on the {ResultLayer.ToMessage()}";
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
    {
        return ResultLayer is ResultLayer.Unknown 
            ? new ResultError(FailureType, layer, Because, OutputType) 
            : this;
    }
}

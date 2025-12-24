using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the result of a mapping operation.
/// Inherits from InfraConvertible to provide conversion capabilities.
/// Used to indicate success or failure of mapping operations.
/// </summary>
public class MapperResult : InfraConvertible, IResultFactory<MapperResult>
{
    private MapperResult(IResultStatus resultStatus) 
        : base(resultStatus)
    {
    }
    
    private MapperResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Unknown, because))
    {
    }

    private MapperResult() 
        : base(ResultLayer.Unknown)
    {
    }
    
    /// <summary>
    /// Converts this MapperResult to a typed MapperResult of type T.
    /// The result must be a failure or the conversion will throw an exception.
    /// </summary>
    /// <typeparam name="T">
    /// The type to which the MapperResult should be converted.
    /// </typeparam>
    /// <returns>
    /// A MapperResult of type T representing the same result status as this instance.
    /// </returns>
    public MapperResult<T> ToTypedMapperResult<T>()
    {
        return MapperResult<T>.From(this);
    }

    /// <summary>
    /// A factory method to create a successful <see cref="MapperResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="MapperResult"/> indicating a successful operation.
    /// </returns>
    public static MapperResult Pass()
    {
        return new MapperResult();
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="MapperResult"/> with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> indicating a failed operation.
    /// </returns>
    public static MapperResult Fail(string because = "")
    {
        return new MapperResult(OperationStatus.Failure(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult"/> representing a domain violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the domain violation.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> indicating a domain violation.
    /// </returns>
    public static MapperResult DomainViolation(string because = "")
    {
        return new MapperResult(OperationStatus.DomainViolation(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult"/> representing an invariant violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the invariant violation.
    /// </param>
    /// <returns></returns>
    public static MapperResult InvariantViolation(string because = "")
    {
        return new MapperResult(OperationStatus.InvariantViolation(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult"/> representing invalid input, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the invalid input.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> indicating invalid input.
    /// </returns>
    public static MapperResult InvalidInput(string because = "")
    {
        return new MapperResult(OperationStatus.InvalidInput(), because);
    }
    
    /// <summary>
    /// A factory method to merge multiple <see cref="IResultStatus"/> instances into a single <see cref="MapperResult"/>.
    /// </summary>
    /// <param name="results">
    /// The collection of <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> representing the merged result of the provided statuses.
    /// </returns>
    public static MapperResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<MapperResult>();
    }
    
    /// <summary>
    /// A factory method to copy of the given <see cref="MapperResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="MapperResult"/> to copy.
    /// </param>
    /// <returns></returns>
    public static MapperResult Copy(MapperResult result)
    {
        return From(result);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="status">
    /// The <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> representing the same result status as the provided instance.
    /// </returns>
    public static MapperResult From(IResultStatus status)
    {
        return new MapperResult(status);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="MapperResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ITypedResult{T}"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> representing the same result status as the provided instance.
    /// </returns>
    public static MapperResult<T> From<T>(ITypedResult<T> result)
    {
        return MapperResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="MapperResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// The result must be a failure or the conversion will throw an exception.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> representing the same result status as the provided instance.
    /// </returns>
    public static MapperResult<T> From<T>(IResultStatus result)
    {
        return MapperResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="MapperResult{T}"/> with the given value.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="MapperResult{T}"/> indicating a successful operation with the provided value.
    /// </returns>
    public static MapperResult<T> Pass<T>(T value)
    {
        return MapperResult<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="MapperResult{T}"/> with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been contained in a successful result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> indicating a failed operation.
    /// </returns>
    public static MapperResult<T> Fail<T>(string because = "")
    {
        return MapperResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult{T}"/> representing a domain violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the domain violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been contained in a successful result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> indicating a domain violation.
    /// </returns>
    public static MapperResult<T> DomainViolation<T>(string because = "")
    {
        return MapperResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult{T}"/> representing invalid input, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the invalid input.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been contained in a successful result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> indicating invalid input.
    /// </returns>
    public static MapperResult<T> InvalidInput<T>(string because = "")
    {
        return MapperResult<T>.Fail(OperationStatus.InvalidInput<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="MapperResult{T}"/> representing an invariant violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason for the invariant violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been contained in a successful result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> indicating an invariant violation.
    /// </returns>
    public static MapperResult<T> InvariantViolation<T>(string because = "")
    {
        return MapperResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to copy of the given <see cref="MapperResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="MapperResult{T}"/> to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns>
    /// A copy of the provided <see cref="MapperResult{T}"/>.
    /// </returns>
    public static MapperResult<T> Copy<T>(MapperResult<T> result)
    {
        return MapperResult<T>.From(result);
    }
}

/// <summary>
/// Represents the result of a mapping operation with a typed value.
/// Inherits from InfraConvertible to provide conversion capabilities.
/// Used to indicate success or failure of mapping operations that yield a value of type T.
/// </summary>
/// <typeparam name="T">
/// The type of the value contained in the result.
/// </typeparam>
public class MapperResult<T> : InfraConvertible<T>
{
    private MapperResult(T value) : base(value, ResultLayer.Unknown)
    {
    }

    private MapperResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Unknown, because))
    {
    }
    
    private MapperResult(ITypedResult<T> result) 
        : base(result)
    {
    }
    
    private MapperResult(IResultStatus result) 
        : base(result)
    {
    }
    
    /// <summary>
    /// Removes the type information from this <see cref="MapperResult{T}"/>, returning a non-typed <see cref="MapperResult"/>.
    /// </summary>
    /// <returns></returns>
    public MapperResult RemoveType()
    {
        return MapperResult.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts this <see cref="MapperResult{T}"/> to a typed MapperResult of type T2.
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public MapperResult<T2> ToTypedMapperResult<T2>()
    {
        return MapperResult<T2>.From(this);
    }

    internal static MapperResult<T> Pass(T value)
    {
        return new MapperResult<T>(value);
    }

    internal static MapperResult<T> Fail(FailedOperationStatus failedOperationStatus, string because = "")
    {
        return new MapperResult<T>(failedOperationStatus, because);
    }
    
    internal static MapperResult<T> From(ITypedResult<T> result)
    {
        return new MapperResult<T>(result);
    }
    
    internal static MapperResult<T> From(IResultStatus result)
    {
        return new MapperResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type T to a successful <see cref="MapperResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="MapperResult{T}"/> indicating a successful operation with the provided value.
    /// </returns>
    public static implicit operator MapperResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="MapperResult{T}"/> to a non-typed <see cref="MapperResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="MapperResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult"/> representing the same result status as the provided instance.
    /// </returns>
    public static implicit operator MapperResult(MapperResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts an <see cref="UntypedResult"/> to a <see cref="MapperResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="UntypedResult"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="MapperResult{T}"/> representing the same result status as the provided instance.
    /// </returns>
    public static implicit operator MapperResult<T>(UntypedResult result)
    {
        return new MapperResult<T>(result);
    }
}
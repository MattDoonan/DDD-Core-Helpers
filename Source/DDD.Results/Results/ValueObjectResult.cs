using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Factory for creating <see cref="ValueObjectResult{T}"/> instances.
/// </summary>
public static class ValueObjectResult
{
    /// <summary>
    /// A factory method to create a successful <see cref="ValueObjectResult{T}"/> containing the provided value.
    /// </summary>
    /// <param name="value">
    /// The value to be encapsulated in the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing a successful operation with the provided value.
    /// </returns>
    public static ValueObjectResult<T> Pass<T>(T value)
    {
        return ValueObjectResult<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="ValueObjectResult{T}"/> with a generic failure status.
    /// </summary>
    /// <param name="because">
    /// An optional explanation for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been returned on success.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing a failed operation.
    /// </returns>
    public static ValueObjectResult<T> Fail<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="ValueObjectResult{T}"/> indicating a domain violation.
    /// </summary>
    /// <param name="because">
    /// An optional explanation for the domain violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been returned on success.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing a failed operation due to a domain violation.
    /// </returns>
    public static ValueObjectResult<T> DomainViolation<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="ValueObjectResult{T}"/> indicating invalid input.
    /// </summary>
    /// <param name="because">
    /// An optional explanation for the invalid input.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value that would have been returned on success.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing a failed operation due to invalid input.
    /// </returns>
    public static ValueObjectResult<T> InvalidInput<T>(string because = "")
    {
        return ValueObjectResult<T>.Fail(OperationStatus.InvalidInput<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a copy of an existing <see cref="ValueObjectResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing result to be copied.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns>
    /// A new <see cref="ValueObjectResult{T}"/> that is a copy of the provided result.
    /// </returns>
    public static ValueObjectResult<T> Copy<T>(ValueObjectResult<T> result)
    {
        return ValueObjectResult<T>.Create(result);
    }
}


/// <summary>
/// Represents the result of an operation that yields a value object of type <typeparamref name="T"/>.
/// It can encapsulate either a successful value or an error status.
/// Inherits from <see cref="EntityConvertible{T}"/> to provide conversion capabilities.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ValueObjectResult<T> : EntityConvertible<T>
{
    private ValueObjectResult(T value) 
        : base(value, ResultLayer.Unknown)
    {
    }
    
    private ValueObjectResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Unknown, because))
    {
    }
    
    private ValueObjectResult(IEntityConvertible<T> result) : base(result)
    {
    }
    
    private ValueObjectResult(IEntityConvertible result) : base(result)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="ValueObjectResult{T}"/> to a typed <see cref="ValueObjectResult{T2}"/>.
    /// </summary>
    /// <typeparam name="T2">
    /// The target type for the conversion.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ValueObjectResult{T2}"/> representing the converted result.
    /// </returns>
    public ValueObjectResult<T2> ToTypedValueObjectResult<T2>()
    {
        return ValueObjectResult<T2>.Create(this);
    }

    internal static ValueObjectResult<T> Pass(T value)
    {
        return new ValueObjectResult<T>(value);
    }

    internal static ValueObjectResult<T> Fail(FailedOperationStatus failedOperationStatus, string because = "")
    {
        return new ValueObjectResult<T>(failedOperationStatus, because);
    }
    
    internal static ValueObjectResult<T> Create(IEntityConvertible<T> result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    private static ValueObjectResult<T> Create(IEntityConvertible result)
    {
        return new ValueObjectResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="ValueObjectResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value to be encapsulated in the successful result.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing a successful operation with the provided value.
    /// </returns>
    public static implicit operator ValueObjectResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts an <see cref="EntityConvertible"/> to a <see cref="ValueObjectResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="EntityConvertible"/> to be converted.
    /// </param>
    /// <returns>
    /// A <see cref="ValueObjectResult{T}"/> representing the converted result.
    /// </returns>
    public static implicit operator ValueObjectResult<T>(EntityConvertible result)
    {
        return new ValueObjectResult<T>(result);
    }
    
}
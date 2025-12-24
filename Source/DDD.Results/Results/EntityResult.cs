using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// An <see cref="EntityResult"/> represents the outcome of an operation related to an entity in the domain.
/// It can indicate success or various types of failures, providing context about the result.
/// </summary>
public class EntityResult : MapperConvertible, IResultFactory<EntityResult>
{
    private EntityResult(IResultStatus resultStatus) 
        : base(resultStatus)
    {
    }
    
    private EntityResult(FailedOperationStatus operationStatus, string? because) 
        : base(new ResultError(operationStatus, ResultLayer.Unknown, because))
    {
    }

    private EntityResult() 
        : base(ResultLayer.Unknown)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="EntityResult"/> to a typed EntityResult of type T: <see cref="EntityResult{T}"/>.
    /// The entity result must be a failure; otherwise, an exception will be thrown.
    /// </summary>
    /// <typeparam name="T">
    /// The type to which the <see cref="EntityResult"/> should be converted.
    /// </typeparam>
    /// <returns></returns>
    public EntityResult<T> ToTypedEntityResult<T>()
    {
        return EntityResult<T>.From(this);
    }
    
    /// <summary>
    /// A factory method that creates a successful <see cref="EntityResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a successful operation.
    /// </returns>
    public static EntityResult Pass()
    {
        return new EntityResult();
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult"/> with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a failed operation.
    /// </returns>
    public static EntityResult Fail(string? because = null)
    {
        return new EntityResult(OperationStatus.Failure(), because);
    }
    
    /// <summary>
    /// A factory method that creates a copy of an existing <see cref="EntityResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="EntityResult"/> to be copied.
    /// </param>
    /// <returns></returns>
    public static EntityResult Copy(EntityResult result)
    {
        return new EntityResult(result);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult"/> due to a domain violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the domain violation occurred. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a domain violation failure.`
    /// </returns>
    public static EntityResult DomainViolation(string? because = null)
    {
        return new EntityResult(OperationStatus.DomainViolation(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult"/> due to invalid input, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation was considered invalid. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating an invalid input failure.
    /// </returns>
    public static EntityResult InvalidInput(string? because = null)
    {
        return new EntityResult(OperationStatus.InvalidInput(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult"/>
    /// indicating that the operation failed because it could not find an object, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the object was not found. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a not found failure.
    /// </returns>
    public static EntityResult NotFound(string? because = null)
    {
        return new EntityResult(OperationStatus.NotFound(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult"/> due to an invariant violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the invariant was violated. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating an invariant violation failure.
    /// </returns>
    public static EntityResult InvariantViolation(string? because = null)
    {
        return new EntityResult(OperationStatus.InvariantViolation(), because);
    }
    
    /// <summary>
    /// A factory method to merge multiple <see cref="IResultStatus"/> results into a single <see cref="EntityResult"/>.
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    public static EntityResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<EntityResult>();
    }
    
    /// <summary>
    /// A factory method that creates an <see cref="EntityResult"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert into an <see cref="EntityResult"/>.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> based on the provided <see cref="IResultStatus"/>.
    /// </returns>
    public static EntityResult From(IResultStatus result)
    {
        return new EntityResult(result);
    }
    
    /// <summary>
    /// A factory method that creates a typed <see cref="EntityResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ITypedResult{T}"/> to convert into an <see cref="EntityResult{T}"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns></returns>
    public static EntityResult<T> From<T>(ITypedResult<T> result)
    {
        return EntityResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method that creates a typed <see cref="EntityResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// An exception will be thrown if the result is successful.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert into an <see cref="EntityResult{T}"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> based on the provided <see cref="IResultStatus"/>.
    /// </returns>
    public static EntityResult<T> From<T>(IResultStatus result)
    {
        return EntityResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method that creates a successful <see cref="EntityResult{T}"/> with the provided value.
    /// </summary>
    /// <param name="value">
    /// The output value of type T for the successful entity result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns></returns>
    public static EntityResult<T> Pass<T>(T value)
    {
        return EntityResult<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult{T}"/> with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating a failed operation.
    /// </returns>
    public static EntityResult<T> Fail<T>(string? because = null)
    {
        return EntityResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult{T}"/> due to a domain violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the domain violation occurred. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating a domain violation failure.
    /// </returns>
    public static EntityResult<T> DomainViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult{T}"/> due to invalid input, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the input was considered invalid. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating an invalid input failure.
    /// </returns>
    public static EntityResult<T> InvalidInput<T>(string? because = null)
    {
        return EntityResult<T>.Fail(OperationStatus.InvalidInput<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult{T}"/> due to not finding an object, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the object was not found. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating a not found failure.
    /// </returns>
    public static EntityResult<T> NotFound<T>(string? because = null)
    {
        return EntityResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="EntityResult{T}"/> due to an invariant violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the invariant was violated. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating an invariant violation failure.
    /// </returns>
    public static EntityResult<T> InvariantViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a copy of an existing <see cref="EntityResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="EntityResult{T}"/> to be copied.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output value for the typed entity result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> that is a copy of the provided result.
    /// </returns>
    public static EntityResult<T> Copy<T>(EntityResult<T> result)
    {
        return EntityResult<T>.From(result);
    }
}

/// <summary>
/// A typed version of <see cref="EntityResult"/> that carries an output value of type T when successful.
/// </summary>
/// <typeparam name="T">
/// The type of the output value for the typed entity result.
/// </typeparam>
public class EntityResult<T> : MapperConvertible<T>
{
    private EntityResult(T value) 
        : base(value, ResultLayer.Unknown)
    {
    }

    private EntityResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Unknown, because))
    {
    }
    
    private EntityResult(ITypedResult<T> result) : base(result)
    {
    }
    
    private EntityResult(IResultStatus result) : base(result)
    {
    }
    
    
    /// <summary>
    /// Converts this <see cref="EntityResult{T}"/> to a non-typed EntityResult: <see cref="EntityResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> without the output value.
    /// </returns>
    public EntityResult RemoveType()
    {
        return EntityResult.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts this <see cref="EntityResult{T}"/> to another typed EntityResult of type T2: <see cref="EntityResult{T2}"/>.
    /// </summary>
    /// <typeparam name="T2">
    /// The type to which the <see cref="EntityResult{T}"/> should be converted.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T2}"/> based on this result.
    /// </returns>
    public EntityResult<T2> ToTypedEntityResult<T2>()
    {
        return EntityResult<T2>.From(this);
    }
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new EntityResult<T>(failedOperationStatus, because);
    }
    
    internal static EntityResult<T> From(ITypedResult<T> result)
    {
        return new EntityResult<T>(result);
    }
    
    internal static EntityResult<T> From(IResultStatus result)
    {
        return new EntityResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type T to a successful <see cref="EntityResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value of type T to be wrapped in a successful EntityResult.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> indicating a successful operation with the provided value.
    /// </returns>
    public static implicit operator EntityResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="EntityResult{T}"/> to a non-typed <see cref="EntityResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="EntityResult{T}"/> to be converted to a non-typed EntityResult.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> without the output value.
    /// </returns>
    public static implicit operator EntityResult(EntityResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="EntityResult"/> to a typed <see cref="EntityResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="EntityResult"/> to be converted to a typed EntityResult.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult{T}"/> based on the provided <see cref="EntityResult"/>.
    /// </returns>
    public static implicit operator EntityResult<T>(EntityResult result)
    {
        return new EntityResult<T>(result);
    }
}
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Convertibles.Interfaces;
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
    
    private EntityResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Unknown, because)
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
    /// A factory method to create a successful <see cref="EntityResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a successful operation.
    /// </returns>
    public static EntityResult Pass()
    {
        return new EntityResult();
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="EntityResult"/> with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a failed operation.
    /// </returns>
    public static EntityResult Fail(string? because = null)
    {
        return new EntityResult(FailureType.Generic, because);
    }
    
    /// <summary>
    /// A factory method to create a copy of an existing <see cref="EntityResult"/>.
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
    /// A factory method to create a failed <see cref="EntityResult"/> due to a domain violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the domain violation occurred. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating a domain violation failure.`
    /// </returns>
    public static EntityResult DomainViolation(string? because = null)
    {
        return new EntityResult(FailureType.DomainViolation, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="EntityResult"/> due to invalid input, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the input was considered invalid. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating an invalid input failure.
    /// </returns>
    public static EntityResult InvalidInput(string? because = null)
    {
        return new EntityResult(FailureType.InvalidInput, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="EntityResult"/>
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
        return new EntityResult(FailureType.NotFound, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="EntityResult"/> due to an invariant violation, with an optional reason.
    /// </summary>
    /// <param name="because">
    /// The reason why the invariant was violated. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="EntityResult"/> indicating an invariant violation failure.
    /// </returns>
    public static EntityResult InvariantViolation(string? because = null)
    {
        return new EntityResult(FailureType.InvariantViolation, because);
    }
    
    /// <summary>
    /// Merges multiple <see cref="IResultStatus"/> results into a single <see cref="EntityResult"/>.
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    public static EntityResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<EntityResult>();
    }
    
    public static EntityResult From(IResultStatus result)
    {
        return new EntityResult(result);
    }
    
    public static EntityResult<T> From<T>(ITypedResult<T> result)
    {
        return EntityResult<T>.From(result);
    }
    
    public static EntityResult<T> From<T>(IResultStatus result)
    {
        return EntityResult<T>.From(result);
    }
    
    public static EntityResult<T> Pass<T>(T value)
    {
        return EntityResult<T>.Pass(value);
    }
    
    public static EntityResult<T> Fail<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.Generic, because);
    }
    
    public static EntityResult<T> DomainViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.DomainViolation, because);
    }
    
    public static EntityResult<T> InvalidInput<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.InvalidInput, because);
    }
    
    public static EntityResult<T> NotFound<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.NotFound, because);
    }
    
    public static EntityResult<T> InvariantViolation<T>(string? because = null)
    {
        return EntityResult<T>.Fail(FailureType.InvariantViolation, because);
    }
    
    public static EntityResult<T> Copy<T>(EntityResult<T> result)
    {
        return EntityResult<T>.From(result);
    }
}

public class EntityResult<T> : MapperConvertible<T>
{
    private EntityResult(T value) 
        : base(value, ResultLayer.Unknown)
    {
    }

    private EntityResult(FailureType failureType, string? because) 
        : base(failureType, ResultLayer.Unknown, because)
    {
    }
    
    private EntityResult(ITypedResult<T> result) : base(result)
    {
    }
    
    private EntityResult(IResultStatus result) : base(result)
    {
    }
    
    public EntityResult RemoveType()
    {
        return EntityResult.From((IResultStatus)this);
    }
    
    public EntityResult<T2> ToTypedEntityResult<T2>()
    {
        return EntityResult<T2>.From(this);
    }
    
    internal static EntityResult<T> Pass(T value)
    {
        return new EntityResult<T>(value);
    }

    internal static EntityResult<T> Fail(FailureType failureType, string? because = null)
    {
        return new EntityResult<T>(failureType, because);
    }
    
    internal static EntityResult<T> From(ITypedResult<T> result)
    {
        return new EntityResult<T>(result);
    }
    
    internal static EntityResult<T> From(IResultStatus result)
    {
        return new EntityResult<T>(result);
    }
    
    public static implicit operator EntityResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator EntityResult(EntityResult<T> result)
    {
        return result.RemoveType();
    }
    
    public static implicit operator EntityResult<T>(MapperConvertible result)
    {
        return new EntityResult<T>(result);
    }
}
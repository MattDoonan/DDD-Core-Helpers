using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the result of a use case operation.
/// Inherits from <see cref="ResultConvertible"/> to provide conversion capabilities.
/// Provides factory methods to create instances of <see cref="UseCaseResult"/> and <see cref="UseCaseResult{T}"/>.
/// </summary>
public class UseCaseResult : ResultConvertible, IResultFactory<UseCaseResult>
{
    private UseCaseResult() 
        : base(ResultLayer.UseCase)
    {
    }
    private UseCaseResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.UseCase, because))
    {
    }
    
    /// <summary>
    /// Converts this <see cref="UseCaseResult"/> to a typed <see cref="UseCaseResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing the same result status as this instance.
    /// </returns>
    public UseCaseResult<T> ToTypedUseCaseResult<T>()
    {
        return UseCaseResult<T>.From(this);
    }

    /// <summary>
    /// A factory method to create a passing <see cref="UseCaseResult"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing a successful operation.
    /// </returns>
    public static UseCaseResult Pass()
    {
        return new UseCaseResult();
    }

    /// <summary>
    /// A factory method to create a failing <see cref="UseCaseResult"/>.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing a failed operation.
    /// </returns>
    public static UseCaseResult Fail(string because = "")
    {
        return new UseCaseResult(OperationStatus.Failure(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="UseCaseResult"/> representing an invariant violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the invariant violation.
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing an invariant violation.
    /// </returns>
    public static UseCaseResult InvariantViolation(string because = "")
    {
        return new UseCaseResult(OperationStatus.InvariantViolation(), because);
    }
    
    /// <summary>
    /// A factory method to merge multiple <see cref="IResultStatus"/> instances into a single <see cref="UseCaseResult"/>.
    /// </summary>
    /// <param name="results">
    /// The <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns></returns>
    public static UseCaseResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<UseCaseResult>();
    }
    
    /// <summary>
    /// A factory method to create a <see cref="UseCaseResult"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing the same result status as the provided <see cref="IResultStatus"/>.
    /// </returns>
    public static UseCaseResult From(IResultStatus result)
    {
        return new UseCaseResult(result);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="UseCaseResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ITypedResult{T}"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing the same result status as the provided <see cref="ITypedResult{T}"/>.
    /// </returns>
    public static UseCaseResult<T> From<T>(ITypedResult<T> result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="UseCaseResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing the same result status as the provided <see cref="IResultStatus"/>.
    /// </returns>
    public static UseCaseResult<T> From<T>(IResultStatus result)
    {
        return UseCaseResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a passing <see cref="UseCaseResult{T}"/> with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the passing result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing a successful operation with the specified value.
    /// </returns>
    public static UseCaseResult<T> Pass<T>(T value)
    {
        return UseCaseResult<T>.Pass(value);
    }

    /// <summary>
    /// A factory method to create a failing <see cref="UseCaseResult{T}"/>.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing a failed operation.
    /// </returns>
    public static UseCaseResult<T> Fail<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="UseCaseResult{T}"/> representing an invariant violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the invariant violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing an invariant violation.
    /// </returns>
    public static UseCaseResult<T> InvariantViolation<T>(string because = "")
    {
        return UseCaseResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a copy of an existing <see cref="UseCaseResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="UseCaseResult"/> to copy.
    /// </param>
    /// <returns>
    /// A new <see cref="UseCaseResult"/> instance representing the same result status as the provided <see cref="UseCaseResult"/>.
    /// </returns>
    public static UseCaseResult Copy(UseCaseResult result)
    {
        return From(result);
    }
    
    /// <summary>
    /// A factory method to create a copy of an existing <see cref="UseCaseResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="UseCaseResult{T}"/> to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T}"/>.
    /// </typeparam>
    /// <returns></returns>
    public static UseCaseResult<T> Copy<T>(UseCaseResult<T> result)
    {
        return UseCaseResult<T>.From(result);
    }
}

/// <summary>
/// Represents the result of a use case operation that produces a value of type <typeparamref name="T"/>.
/// Inherits from <see cref="ResultConvertible{T}"/> to provide conversion capabilities.
/// </summary>
/// <typeparam name="T">
/// The type of the value contained in the result.
/// </typeparam>
public class UseCaseResult<T> : ResultConvertible<T>
{
    private UseCaseResult(T value) 
        : base(value, ResultLayer.UseCase)
    {
    }

    private UseCaseResult(FailedOperationStatus failedOperationStatus, string because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.UseCase, because))
    {
    }

    private UseCaseResult(ITypedResult<T> result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    private UseCaseResult(IResultStatus result) 
        : base(result, ResultLayer.UseCase)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="UseCaseResult{T}"/> to a non-typed <see cref="UseCaseResult"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing the same result status as this instance.
    /// </returns>
    public UseCaseResult RemoveType()
    {
        return UseCaseResult.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts this <see cref="UseCaseResult{T}"/> to a different typed <see cref="UseCaseResult{T2}"/>.
    /// </summary>
    /// <typeparam name="T2">
    /// The type of the value contained in the resulting <see cref="UseCaseResult{T2}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="UseCaseResult{T2}"/> instance representing the same result status as this instance.
    /// </returns>
    public UseCaseResult<T2> ToTypedUseCaseResult<T2>()
    {
        return UseCaseResult<T2>.From(this);
    }
    
    internal static UseCaseResult<T> Pass(T value)
    {
        return new UseCaseResult<T>(value);
    }

    internal static UseCaseResult<T> Fail(FailedOperationStatus failedOperationStatus, string because = "")
    {
        return new UseCaseResult<T>(failedOperationStatus, because);
    }
    
    internal static UseCaseResult<T> From(ITypedResult<T> result)
    {
        return new UseCaseResult<T>(result);
    }
    
    internal static UseCaseResult<T> From(IResultStatus result)
    {
        return new UseCaseResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a passing <see cref="UseCaseResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the passing result.
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing a successful operation with the specified value.
    /// </returns>
    public static implicit operator UseCaseResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="UseCaseResult{T}"/> to a non-typed <see cref="UseCaseResult"/>.
    /// </summary>
    /// <param name="result">
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult"/> instance representing the same result status as the provided <see cref="UseCaseResult{T}"/>.
    /// </returns>
    public static implicit operator UseCaseResult(UseCaseResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts a non-typed <see cref="UseCaseResult"/> to a <see cref="UseCaseResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="UseCaseResult"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="UseCaseResult{T}"/> instance representing the same result status as the provided <see cref="UseCaseResult"/>.
    /// </returns>
    public static implicit operator UseCaseResult<T>(UseCaseResult result)
    {
        return new UseCaseResult<T>(result);
    }
}
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the result of a repository operation.
/// Inherits from <see cref="ServiceConvertible"/> to provide conversion capabilities.
/// Provides factory methods to create instances of <see cref="RepoResult"/> and <see cref="RepoResult{T}"/>.
/// </summary>
public class RepoResult : ServiceConvertible, IResultFactory<RepoResult>
{
    
    private RepoResult() 
        : base(ResultLayer.Infrastructure)
    {
    }
    private RepoResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    
    /// <summary>
    /// Converts this <see cref="RepoResult"/> to a typed <see cref="RepoResult{T}"/>.
    /// Must be a failure result to convert otherwise an exception will be thrown.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public RepoResult<T> ToTypedRepoResult<T>()
    {
        return RepoResult<T>.From(this);
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="RepoResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a successful operation.
    /// </returns>
    public static RepoResult Pass()
    {
        return new RepoResult();
    }

    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/>.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the failure.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a failed operation.
    /// </returns>
    public static RepoResult Fail(string? because = null)
    {
        return new RepoResult(OperationStatus.Failure(), because);
    }

    /// <summary>
    /// A factory method to create a copy of an existing <see cref="RepoResult"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="RepoResult"/> to copy.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> that is a copy of the provided result.
    /// </returns>
    public static RepoResult Copy(RepoResult result)
    {
        return new RepoResult(result);
    }

    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating that the requested entity was not found.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the not found status.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a not found operation.
    /// </returns>
    public static RepoResult NotFound(string? because = null)
    {
        return new RepoResult(OperationStatus.NotFound(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating that the entity already exists.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the already exists status.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing an already exists operation.
    /// </returns>
    public static RepoResult AlreadyExists(string? because = null)
    {
        return new RepoResult(OperationStatus.AlreadyExists(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating that the request was invalid.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the invalid request status.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing an invalid request operation.
    /// </returns>
    public static RepoResult InvalidRequest(string? because = null)
    {
        return new RepoResult(OperationStatus.InvalidRequest(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating a concurrency violation.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the concurrency violation.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a concurrency violation operation.
    /// </returns>
    public static RepoResult ConcurrencyViolation(string? because = null)
    {
        return new RepoResult(OperationStatus.ConcurrencyViolation(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating that the operation timed out.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the timeout.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a timeout operation.
    /// </returns>
    public static RepoResult OperationTimeout(string? because = null)
    {
        return new RepoResult(OperationStatus.TimedOut(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> indicating that the operation was cancelled.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the cancellation.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing a cancelled operation.
    /// </returns>
    public static RepoResult OperationCancelled(string? because = null)
    {
        return new RepoResult(OperationStatus.Cancelled(), because);
    }
    
    /// <summary>
    /// A factory method to merge multiple <see cref="IResultStatus"/> instances into a single <see cref="RepoResult"/>.
    /// </summary>
    /// <param name="results">
    /// The <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing the merged result.
    /// </returns>
    public static RepoResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<RepoResult>();
    }

    /// <summary>
    /// A factory method to create a <see cref="RepoResult"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="ITypedResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing the provided result.
    /// </returns>
    public static RepoResult From(IResultStatus result)
    {
        return new RepoResult(result);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="RepoResult"/> from an exception.
    /// </summary>
    /// <param name="exception">
    /// The exception that caused the failure.
    /// </param>
    /// <param name="operationStatus">
    /// An optional specific failed operation status. If not provided, a generic failure status will be used.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing the failure caused by the exception.
    /// </returns>
    public static RepoResult From(Exception exception, FailedOperationStatus? operationStatus = null)
    {
        return new RepoResult(operationStatus ?? OperationStatus.Failure(), exception.ToResultMessage());
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="RepoResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="ITypedResult{T}"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns></returns>
    public static RepoResult<T> From<T>(ITypedResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="RepoResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing the provided result.
    /// </returns>
    public static RepoResult<T> From<T>(IResultStatus result)
    {
        return RepoResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a successful typed <see cref="RepoResult{T}"/> with the provided value.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a successful operation with the provided value.
    /// </returns>
    public static RepoResult<T> Pass<T>(T value)
    {
        return RepoResult<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/>.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a failed operation.
    /// </returns>
    public static RepoResult<T> Fail<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating that the requested entity was not found.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the not found status.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a not found operation.
    /// </returns>
    public static RepoResult<T> NotFound<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating that the entity already exists.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the already exists status.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing an already exists operation.
    /// </returns>
    public static RepoResult<T> AlreadyExists<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.AlreadyExists<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating that the request was invalid.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the invalid request status.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing an invalid request operation.
    /// </returns>
    public static RepoResult<T> InvalidRequest<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.InvalidRequest<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating a concurrency violation.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the concurrency violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a concurrency violation operation.
    /// </returns>
    public static RepoResult<T> ConcurrencyViolation<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.ConcurrencyViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating that the operation timed out.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the timeout.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a timeout operation.
    /// </returns>
    public static RepoResult<T> OperationTimeout<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.TimedOut<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> indicating that the operation was cancelled.
    /// </summary>
    /// <param name="because">
    /// An optional message explaining the reason for the cancellation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a cancelled operation.
    /// </returns>
    public static RepoResult<T> OperationCancelled<T>(string? because = null)
    {
        return RepoResult<T>.Fail(OperationStatus.Cancelled<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a copy of an existing typed <see cref="RepoResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="RepoResult{T}"/> to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> that is a copy of the provided result.
    /// </returns>
    public static RepoResult<T> Copy<T>(RepoResult<T> result)
    {
        return RepoResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a failed typed <see cref="RepoResult{T}"/> from an exception.
    /// </summary>
    /// <param name="exception">
    /// The exception that caused the failure.
    /// </param>
    /// <param name="operationStatus">
    /// An optional specific failed operation status. If not provided, a generic failure status will be used.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing the failure caused by the exception.
    /// </returns>
    public static RepoResult<T> From<T>(Exception exception, FailedOperationStatus? operationStatus = null)
    {
        return RepoResult<T>.Fail(operationStatus ?? OperationStatus.Failure<T>(), exception.ToResultMessage());
    }
}

/// <summary>
/// Represents the result of a repository operation that returns a value of type <typeparamref name="T"/>.
/// Inherits from <see cref="ServiceConvertible{T}"/> to provide conversion capabilities.
/// </summary>
/// <typeparam name="T"></typeparam>
public class RepoResult<T> : ServiceConvertible<T>
{
    private RepoResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    private RepoResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    private RepoResult(IResultStatus result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="RepoResult{T}"/> to a non-typed <see cref="RepoResult"/>.
    /// </summary>
    /// <returns></returns>
    public RepoResult RemoveType()
    {
        return RepoResult.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts this <see cref="RepoResult{T}"/> to a typed <see cref="RepoResult{T2}"/>.
    /// </summary>
    /// <typeparam name="T2">
    /// The type of the value in the resulting <see cref="RepoResult{T2}"/>.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T2}"/> representing the same result status as this instance.
    /// </returns>
    public RepoResult<T2> ToTypedRepoResult<T2>()
    {
        return RepoResult<T2>.From(this);
    }
    
    internal static RepoResult<T> Pass(T value)
    {
        return new RepoResult<T>(value);
    }

    internal static RepoResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new RepoResult<T>(failedOperationStatus, because);
    }
    
    internal static RepoResult<T> From(ITypedResult<T> result)
    {
        return new RepoResult<T>(result);
    }
    
    internal static RepoResult<T> From(IResultStatus result)
    {
        return new RepoResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="RepoResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing a successful operation with the provided value.
    /// </returns>
    public static implicit operator RepoResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="RepoResult{T}"/> to a non-typed <see cref="RepoResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="RepoResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult"/> representing the same result status as the provided result.
    /// </returns>
    public static implicit operator RepoResult(RepoResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="RepoResult"/> to a <see cref="RepoResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="RepoResult"/> to convert.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="RepoResult{T}"/> representing the provided result.
    /// </returns>
    public static implicit operator RepoResult<T>(RepoResult result)
    {
        return new RepoResult<T>(result);
    }
}
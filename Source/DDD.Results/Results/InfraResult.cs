using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the result of an infrastructure operation.
/// Inherits from <see cref="RepoConvertible"/> and implements IResultFactory for InfraResult.
/// Used for non-repository related infrastructure results.
/// </summary>
public class InfraResult : RepoConvertible, IResultFactory<InfraResult>
{
    private InfraResult() 
        : base(ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(IResultStatus resultStatus) 
        : base(resultStatus, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    /// <summary>
    /// Converts this InfraResult to a typed InfraResult of type T.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new InfraResult of type T based on this InfraResult.
    /// </returns>
    public InfraResult<T> ToTypedInfraResult<T>()
    {
        return InfraResult<T>.From(this);
    }
    
    /// <summary>
    /// A factory method that creates a passing <see cref="InfraResult"/>.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing a successful operation.
    /// </returns>
    public static InfraResult Pass()
    {
        return new InfraResult();
    }

    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult"/>.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing a failed operation.
    /// </returns>
    public static InfraResult Fail(string? because = null)
    {
        return new InfraResult(OperationStatus.Failure(), because);
    }
    
    /// <summary>
    /// A factory method that creates a copy of the given <see cref="InfraResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="InfraResult"/> to copy.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> that is a copy of the provided result.
    /// </returns>
    public static InfraResult Copy(InfraResult result)
    {
        return From(result);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult"/> due to the resource not being found.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed due to the resource not being found. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing a 'Not Found' failure.
    /// </returns>
    public static InfraResult NotFound(string? because = null)
    {
        return new InfraResult(OperationStatus.NotFound(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult"/> due to the resource already existing.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed due to the resource already existing. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing an 'Already Exists' failure.
    /// </returns>
    public static InfraResult AlreadyExists(string? because = null)
    {
        return new InfraResult(OperationStatus.AlreadyExists(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult"/> due to an invalid request.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation was considered invalid. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing an 'Invalid Request' failure.
    /// </returns>
    public static InfraResult InvalidRequest(string? because = null)
    {
        return new InfraResult(OperationStatus.InvalidRequest(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult"/> due to an operation timeout.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation timed out. This parameter is optional.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing an 'Operation Timeout' failure.
    /// </returns>
    public static InfraResult OperationTimeout(string? because = null)
    {
        return new InfraResult(OperationStatus.TimedOut(), because);
    }
    
    /// <summary>
    /// A factory method that merges multiple <see cref="IResultStatus"/> instances into a single <see cref="InfraResult"/>.
    /// </summary>
    /// <param name="results">
    /// The array of <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> representing the merged result.
    /// </returns>
    public static InfraResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<InfraResult>();
    }
    
    /// <summary>
    /// A factory method that creates an <see cref="InfraResult"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert into an <see cref="InfraResult"/>.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult"/> based on the provided result.
    /// </returns>
    public static InfraResult From(IResultStatus result)
    {
        return new InfraResult(result);
    }
    
    /// <summary>
    /// A factory method that creates a typed <see cref="InfraResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ITypedResult{T}"/> to convert into an <see cref="InfraResult{T}"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> based on the provided typed result.
    /// </returns>
    public static InfraResult<T> From<T>(ITypedResult<T> result)
    {
        return InfraResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method that creates a typed <see cref="InfraResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> to convert into an <see cref="InfraResult{T}"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> based on the provided result.
    /// </returns>
    public static InfraResult<T> From<T>(IResultStatus result)
    {
        return InfraResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method that creates a passing <see cref="InfraResult{T}"/> with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to be included in the passing result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing a successful operation with the provided value.
    /// </returns>
    public static InfraResult<T> Pass<T>(T value)
    {
        return InfraResult<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult{T}"/>.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing a failed operation.
    /// </returns>
    public static InfraResult<T> Fail<T>(string? because = null)
    {
        return InfraResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult{T}"/> due to the resource not being found.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed due to the resource not being found. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing a 'Not Found' failure.
    /// </returns>
    public static InfraResult<T> NotFound<T>(string? because = null)
    {
        return InfraResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult{T}"/> due to the resource already existing.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation failed due to the resource already existing. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing an 'Already Exists' failure.
    /// </returns>
    public static InfraResult<T> AlreadyExists<T>(string? because = null)
    {
        return InfraResult<T>.Fail(OperationStatus.AlreadyExists<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult{T}"/> due to an invalid request.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation was considered invalid. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing an 'Invalid Request' failure.
    /// </returns>
    public static InfraResult<T> InvalidRequest<T>(string? because = null)
    {
        return InfraResult<T>.Fail(OperationStatus.InvalidRequest<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a failed <see cref="InfraResult{T}"/> due to an operation timeout.
    /// </summary>
    /// <param name="because">
    /// The reason why the operation timed out. This parameter is optional.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> representing an 'Operation Timeout' failure.
    /// </returns>
    public static InfraResult<T> OperationTimeout<T>(string? because = null)
    {
        return InfraResult<T>.Fail(OperationStatus.TimedOut<T>(), because);
    }
    
    /// <summary>
    /// A factory method that creates a copy of the given <see cref="InfraResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="InfraResult{T}"/> to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the output for the typed InfraResult.
    /// </typeparam>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> that is a copy of the provided result.
    /// </returns>
    public static InfraResult<T> Copy<T>(InfraResult<T> result)
    {
        return InfraResult<T>.From(result);
    }
}

/// <summary>
/// Represents the result of an infrastructure operation with a typed output.
/// Inherits from RepoConvertible and is used for non-repository related infrastructure results.
/// </summary>
/// <typeparam name="T">
/// The type of the output for the typed InfraResult.
/// </typeparam>
public class InfraResult<T> : RepoConvertible<T>
{
    private InfraResult(T value) 
        : base(value, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Infrastructure, because))
    {
    }
    
    private InfraResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    private InfraResult(IResultStatus result) 
        : base(result, ResultLayer.Infrastructure)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="InfraResult{T}"/> to a non-typed <see cref="InfraResult"/>.
    /// </summary>
    /// <returns></returns>
    public InfraResult RemoveType()
    {
        return InfraResult.From((IResultStatus)this);
    }
    
    
    /// <summary>
    /// Converts this InfraResult to a <see cref="InfraResult{T2}"/> of a different type.
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public InfraResult<T2> ToTypedInfraResult<T2>()
    {
        return InfraResult<T2>.From(this);
    }
    
    internal static InfraResult<T> Pass(T value)
    {
        return new InfraResult<T>(value);
    }

    internal static InfraResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new InfraResult<T>(failedOperationStatus, because);
    }
    
    internal static InfraResult<T> From(ITypedResult<T> result)
    {
        return new InfraResult<T>(result);
    }
    
    internal static InfraResult<T> From(IResultStatus result)
    {
        return new InfraResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type T to a passing <see cref="InfraResult{T}"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator InfraResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts an <see cref="InfraResult{T}"/> to a non-typed <see cref="InfraResult"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static implicit operator InfraResult(InfraResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts a RepoConvertible to a typed InfraResult of type T.
    /// </summary>
    /// <param name="result">
    /// The <see cref="InfraResult"/> to convert.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="InfraResult{T}"/> based on the provided result.
    /// </returns>
    public static implicit operator InfraResult<T>(InfraResult result)
    {
        return new InfraResult<T>(result);
    }
}
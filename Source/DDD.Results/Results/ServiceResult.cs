using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Convertibles;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the result of a service layer operation.
/// This class can represent both successful and failed operations,
/// and provides factory methods to create instances for various failure scenarios.
/// </summary>
public class ServiceResult : UseCaseConvertible, IResultFactory<ServiceResult>
{
    private ServiceResult() 
        : base(ResultLayer.Service)
    {
    }
    
    private ServiceResult(IResultStatus resultStatus) 
         : base(resultStatus, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Service, because))
    {
    }
    
    /// <summary>
    /// Converts this <see cref="ServiceResult"/> to a typed <see cref="ServiceResult{T}"/>.
    /// Will throw an exception if the inputted <see cref="ServiceResult"/> is a successful result.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing the same result status as this <see cref="ServiceResult"/>.
    /// </returns>
    public ServiceResult<T> ToTypedServiceResult<T>()
    {
        return ServiceResult<T>.From(this);
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="ServiceResult"/>.
    /// </summary>
    /// <returns></returns>
    public static ServiceResult Pass()
    {
        return new ServiceResult();
    }

    /// <summary>
    /// A factory method to create a failed <see cref="ServiceResult"/>.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing a failed operation.
    /// </returns>
    public static ServiceResult Fail(string? because = null)
    {
        return new ServiceResult(OperationStatus.Failure(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="ServiceResult"/> representing a domain violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the domain violation.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing a domain violation.
    /// </returns>
    public static ServiceResult DomainViolation(string? because = null)
    {
        return new ServiceResult(OperationStatus.DomainViolation(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="ServiceResult"/> representing a not allowed operation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for why the operation is not allowed.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing a not allowed operation.
    /// </returns>
    public static ServiceResult NotAllowed(string? because = null)
    {
        return new ServiceResult(OperationStatus.NotAllowed(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="ServiceResult"/> representing a not found result.
    /// </summary>
    /// <param name="because">
    /// An optional reason for why the entity was not found.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing a not found result.
    /// </returns>
    public static ServiceResult NotFound(string? because = null)
    {
        return new ServiceResult(OperationStatus.NotFound(), because);
    }
    
    /// <summary>
    /// A factory method to create a <see cref="ServiceResult"/> representing an invariant violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the invariant violation.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing an invariant violation.
    /// </returns>
    public static ServiceResult InvariantViolation(string? because = null)
    {
        return new ServiceResult(OperationStatus.InvariantViolation(), because);
    }
    
    
    /// <summary>
    /// A factory method to merge multiple <see cref="IResultStatus"/> instances into a single <see cref="ServiceResult"/>.
    /// </summary>
    /// <param name="results">
    /// The <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing the merged result of the inputted results.
    /// </returns>
    public static ServiceResult Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<ServiceResult>();
    }
    
    /// <summary>
    /// A factory method to create a <see cref="ServiceResult"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ServiceResult From(IResultStatus result)
    {
        return new ServiceResult(result);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> from an existing <see cref="ITypedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="ITypedResult{T}"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing the same result status as the inputted <see cref="ITypedResult{T}"/>.
    /// </returns>
    public static ServiceResult<T> From<T>(ITypedResult<T> result)
    {
        return ServiceResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> from an existing <see cref="IResultStatus"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="IResultStatus"/> to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing the same result status as the inputted <see cref="IResultStatus"/>.
    /// </returns>
    public static ServiceResult<T> From<T>(IResultStatus result)
    {
        return ServiceResult<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a successful typed <see cref="ServiceResult{T}"/> with the given value.
    /// </summary>
    /// <param name="value">
    /// The value to include in the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns></returns>
    public static ServiceResult<T> Pass<T>(T value)
    {
        return ServiceResult<T>.Pass(value);
    }

    /// <summary>
    /// A factory method to create a failed typed <see cref="ServiceResult{T}"/>.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing a failed operation.
    /// </returns>
    public static ServiceResult<T> Fail<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.Failure<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> representing a domain violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the domain violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing a domain violation.
    /// </returns>
    public static ServiceResult<T> DomainViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.DomainViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> representing a not allowed operation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for why the operation is not allowed.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing a not allowed operation.
    /// </returns>
    public static ServiceResult<T> NotAllowed<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.NotAllowed<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> representing a not found result.
    /// </summary>
    /// <param name="because">
    /// An optional reason for why the entity was not found.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing a not found result.
    /// </returns>
    public static ServiceResult<T> NotFound<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.NotFound<T>(), because);
    }
    
    /// <summary>
    /// A factory method to create a typed <see cref="ServiceResult{T}"/> representing an invariant violation.
    /// </summary>
    /// <param name="because">
    /// An optional reason for the invariant violation.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing an invariant violation.
    /// </returns>
    public static ServiceResult<T> InvariantViolation<T>(string? because = null)
    {
        return ServiceResult<T>.Fail(OperationStatus.InvariantViolation<T>(), because);
    }
    
    /// <summary>
    /// A factory method to copy an existing <see cref="ServiceResult"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="ServiceResult"/> to copy.
    /// </param>
    /// <returns>
    /// A new <see cref="ServiceResult"/> instance representing the same result status as the inputted <see cref="ServiceResult"/>.
    /// </returns>
    public static ServiceResult Copy(ServiceResult result)
    {
        return From(result);
    }
    
    /// <summary>
    /// A factory method to copy an existing typed <see cref="ServiceResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The existing <see cref="ServiceResult{T}"/> to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value in the resulting <see cref="ServiceResult{T}"/>.
    /// </typeparam>
    /// <returns>
    /// A new <see cref="ServiceResult{T}"/> instance representing the same result status as the inputted <see cref="ServiceResult{T}"/>.
    /// </returns>
    public static ServiceResult<T> Copy<T>(ServiceResult<T> result)
    {
        return ServiceResult<T>.From(result);
    }
}


/// <summary>
/// Represents the result of a service layer operation that returns a value of type <typeparamref name="T"/>.
/// This class can represent both successful and failed operations,
/// </summary>
/// <typeparam name="T">
/// The type of the value returned by the operation.
/// </typeparam>
public class ServiceResult<T> : UseCaseConvertible<T>
{
    private ServiceResult(T value) 
        : base(value, ResultLayer.Service)
    {
    }
    
    private ServiceResult(FailedOperationStatus failedOperationStatus, string? because) 
        : base(new ResultError(failedOperationStatus, ResultLayer.Service, because))
    {
    }
    
    private ServiceResult(ITypedResult<T> result) 
        : base(result, ResultLayer.Service)
    {
    }
    
    private ServiceResult(IResultStatus result)
        : base(result, ResultLayer.Service)
    {
    }
    
    /// <summary>
    /// Converts this <see cref="ServiceResult{T}"/> to a non-typed <see cref="ServiceResult"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing the same result status as this <see cref="ServiceResult{T}"/>.
    /// </returns>
    public ServiceResult RemoveType()
    {
        return ServiceResult.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts this <see cref="ServiceResult{T}"/> to a typed <see cref="ServiceResult{T2}"/>.
    /// </summary>
    /// <typeparam name="T2">
    /// The type of the value in the resulting <see cref="ServiceResult{T2}"/>.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ServiceResult{T2}"/> representing the same result status as this <see cref="ServiceResult{T}"/>.
    /// </returns>
    public ServiceResult<T2> ToTypedServiceResult<T2>()
    {
        return ServiceResult<T2>.From(this);
    }
    
    internal static ServiceResult<T> Pass(T value)
    {
        return new ServiceResult<T>(value);
    }

    internal static ServiceResult<T> Fail(FailedOperationStatus failedOperationStatus, string? because = null)
    {
        return new ServiceResult<T>(failedOperationStatus, because);
    }
    
    internal static ServiceResult<T> From(ITypedResult<T> result)
    {
        return new ServiceResult<T>(result);
    }
    
    internal static ServiceResult<T> From(IResultStatus result)
    {
        return new ServiceResult<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="ServiceResult{T}"/>.
    /// </summary>
    /// <param name="value">
    /// The value to include in the successful result.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing a successful operation with the given value.
    /// </returns>
    public static implicit operator ServiceResult<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="ServiceResult{T}"/> to a non-typed <see cref="ServiceResult"/>.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ServiceResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// A <see cref="ServiceResult"/> representing the same result status as the inputted <see cref="ServiceResult{T}"/>.
    /// </returns>
    public static implicit operator ServiceResult(ServiceResult<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="ServiceResult"/> to a <see cref="ServiceResult{T}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <returns>
    /// A <see cref="ServiceResult{T}"/> representing the same result status as the inputted <see cref="ServiceResult"/>.
    /// </returns>
    public static implicit operator ServiceResult<T>(ServiceResult result)
    {
        return new ServiceResult<T>(result);
    }
}
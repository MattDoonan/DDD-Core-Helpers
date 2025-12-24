using DDD.Core.Constants;
using DDD.Core.Operations.Statuses.Abstract;
using DDD.Core.Results.Abstract;
using DDD.Core.Results.Extensions;
using DDD.Core.Results.Interfaces;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results;

/// <summary>
/// Represents the outcome of an operation, indicating success or failure.
/// </summary>
public class Result : UntypedResult, IResultFactory<Result>
{
    private Result(ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(resultLayer)
    {
    }
    
    private Result(IResultStatus resultStatus, ResultLayer? newLayer = null) 
        : base(resultStatus, newLayer)
    {
    }
    
    private Result(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because) 
        : base(new ResultError(failedOperationStatus, failedLayer, because))
    {
    }

    /// <summary>
    /// Converts the result to an exit code.
    /// 0 indicates success, while any non-zero value indicates failure.
    /// </summary>
    /// <returns>
    /// An integer exit code representing the result's success or failure.
    /// </returns>
    public int ToExitCode()
    {
        return ExitCode.FromBool(IsSuccessful);
    }
    
    /// <summary>
    /// Converts the untyped result to a typed result of the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type to which the result should be converted.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing the typed result.
    /// </returns>
    public Result<T> ToTypedResult<T>()
    {
        return Result<T>.From(this);
    }

    /// <summary>
    /// A factory method to create a successful <see cref="Result"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> instance representing a successful operation.
    /// </returns>
    public static Result Pass()
    {
        return new Result();
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="Result"/> instance with a specified result layer.
    /// </summary>
    /// <param name="resultLayer">
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a successful operation.
    /// </returns>
    public static Result Pass(ResultLayer resultLayer)
    {
        return new Result(resultLayer);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result"/> instance.
    /// </summary>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation.
    /// </returns>
    public static Result Fail(string because = "")
    {
        return new Result(OperationStatus.Failure(), ResultLayer.Unknown, because);
    }

    /// <summary>
    /// A factory method to create a failed <see cref="Result"/> instance with a specified failed status.
    /// </summary>
    /// <param name="failedStatus">
    /// The specific failed operation status.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation.
    /// </returns>
    public static Result Fail(FailedOperationStatus failedStatus, string because)
    {
        return new Result(failedStatus, ResultLayer.Unknown, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result"/> instance with a specified failed layer.
    /// </summary>
    /// <param name="failedLayer">
    /// The layer where the failure occurred.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation.
    /// </returns>
    public static Result Fail(ResultLayer failedLayer, string because)
    {
        return new Result(OperationStatus.Failure(), failedLayer, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result"/> instance with specified failed status and layer.
    /// </summary>
    /// <param name="failedStatus">
    /// The specific failed operation status.
    /// </param>
    /// <param name="failedLayer">
    /// The layer where the failure occurred.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation.
    /// </returns>
    public static Result Fail(FailedOperationStatus failedStatus, ResultLayer failedLayer, string because)
    {
        return new Result(failedStatus, failedLayer, because);
    }
    
    /// <summary>
    /// Creates a copy of the given <see cref="Result"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="Result"/> instance to copy.
    /// </param>
    /// <returns>
    /// A new <see cref="Result"/> instance that is a copy of the provided result.
    /// </returns>
    public static Result Copy(Result result)
    {
        return new Result(result);
    }
    
    /// <summary>
    /// Creates a <see cref="Result"/> instance from an existing <see cref="IResultStatus"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> instance to convert.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing the provided result status.
    /// </returns>
    public static Result From(IResultStatus result)
    {
        return new Result(result);
    }
    
    /// <summary>
    /// Creates a <see cref="Result"/> instance from an existing <see cref="IResultStatus"/> instance,
    /// assigning it a new result layer.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> instance to convert.
    /// </param>
    /// <param name="newResultLayer">
    /// The new result layer to assign to the created <see cref="Result"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing the provided result status with the specified result layer.
    /// </returns>
    public static Result From(IResultStatus result, ResultLayer newResultLayer)
    {
        return new Result(result, newResultLayer);
    }
    
    /// <summary>
    /// Creates a typed <see cref="Result{T}"/> instance from an existing <see cref="ITypedResult{T}"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="ITypedResult{T}"/> instance to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing the provided typed result.
    /// </returns>
    public static Result<T> From<T>(ITypedResult<T> result)
    {
        return Result<T>.From(result);
    }
    
    /// <summary>
    /// Creates a typed <see cref="Result{T}"/> instance from an existing <see cref="IResultStatus"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IResultStatus"/> instance to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing the provided result status.
    /// </returns>
    public static Result<T> From<T>(IResultStatus result)
    {
        return Result<T>.From(result);
    }
    
    /// <summary>
    /// Merges multiple <see cref="IResultStatus"/> instances into a single <see cref="Result"/> instance.
    /// </summary>
    /// <param name="results">
    /// The <see cref="IResultStatus"/> instances to merge.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing the merged results.
    /// </returns>
    public static Result Merge(params IResultStatus[] results)
    {
        return results.AggregateTo<Result>();
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="Result{T}"/> instance with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a successful operation with the specified value.
    /// </returns>
    public static Result<T> Pass<T>(T value)
    {
        return Result<T>.Pass(value);
    }
    
    /// <summary>
    /// A factory method to create a successful <see cref="Result{T}"/> instance with the specified value and result layer.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <param name="resultLayer">
    /// The result layer associated with the successful result.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a successful operation with the specified value and result layer.
    /// </returns>
    public static Result<T> Pass<T>(T value, ResultLayer resultLayer)
    {
        return Result<T>.Pass(value, resultLayer);
    }
    
    /// <summary>
    /// Creates a copy of the given <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="Result{T}"/> instance to copy.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A new <see cref="Result{T}"/> instance that is a copy of the provided result.
    /// </returns>
    public static Result<T> Copy<T>(Result<T> result)
    {
        return Result<T>.From(result);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a failed operation.
    /// </returns>
    public static Result<T> Fail<T>(string because = "")
    {
        return Result<T>.Fail(OperationStatus.Failure<T>(), ResultLayer.Unknown, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result{T}"/> instance with a specified failed status.
    /// </summary>
    /// <param name="failedStatus">
    /// The specific failed operation status.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a failed operation.
    /// </returns>
    public static Result<T> Fail<T>(FailedOperationStatus failedStatus, string because = "")
    {
        return Result<T>.Fail(failedStatus, ResultLayer.Unknown, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result{T}"/> instance with a specified failed layer.
    /// </summary>
    /// <param name="failedLayer">
    /// The layer where the failure occurred.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a failed operation.
    /// </returns>
    public static Result<T> Fail<T>(ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(OperationStatus.Failure<T>(), failedLayer, because);
    }
    
    /// <summary>
    /// A factory method to create a failed <see cref="Result{T}"/> instance with specified failed status and layer.
    /// </summary>
    /// <param name="failedStatus">
    /// The specific failed operation status.
    /// </param>
    /// <param name="failedLayer">
    /// The layer where the failure occurred.
    /// </param>
    /// <param name="because">
    /// A string describing the reason for the failure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the typed result.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a failed operation.
    /// </returns>
    public static Result<T> Fail<T>(FailedOperationStatus failedStatus, ResultLayer failedLayer, string because = "")
    {
        return Result<T>.Fail(failedStatus, failedLayer, because);
    }
}

/// <summary>
/// Represents the outcome of an operation that returns a value of type T, indicating success or failure.
/// </summary>
/// <typeparam name="T">
/// The type of the value returned by the operation.
/// </typeparam>
public class Result<T> : TypedResult<T>
{
    private Result(T value, ResultLayer resultLayer = ResultLayer.Unknown) 
        : base(value, resultLayer)
    {
    }
    
    private Result(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because) 
        : base(new ResultError(failedOperationStatus, failedLayer, because))
    {
    }
    
    private Result(ITypedResult<T> result) : base(result)
    {
    }
    
    private Result(IResultStatus result) : base(result)
    {
    }
    
    /// <summary>
    /// Converts the result to an exit code.
    /// 0 indicates success, while any non-zero value indicates failure.
    /// </summary>
    /// <returns></returns>
    public int ToExitCode()
    {
        return ExitCode.FromBool(IsSuccessful);
    }
    
    /// <summary>
    /// Removes the type information from the result, returning an untyped <see cref="Result"/> instance.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> instance representing the untyped result.
    /// </returns>
    public Result RemoveType()
    {
        return Result.From((IResultStatus)this);
    }
    
    /// <summary>
    /// Converts the <see cref="Result{T}"/> instance to a typed result of a different specified type.
    /// The result must be a failure or an exception will be thrown.
    /// </summary>
    /// <typeparam name="T2">
    /// The type to which the result should be converted.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Result{T2}"/> instance representing the typed result.
    /// </returns>
    public Result<T2> ToTypedResult<T2>()
    {
        return Result<T2>.From(this);
    }
    
    internal static Result<T> Pass(T value, ResultLayer resultLayer = ResultLayer.Unknown)
    {
        return new Result<T>(value, resultLayer);
    }
    
    internal static Result<T> Fail(FailedOperationStatus failedOperationStatus, ResultLayer failedLayer, string because = "")
    {
        return new Result<T>(failedOperationStatus, failedLayer, because);
    }
    
    internal static Result<T> From(ITypedResult<T> result)
    {
        return new Result<T>(result);
    }
    
    internal static Result<T> From(IResultStatus result)
    {
        return new Result<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a value of type T to a successful <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="value">
    /// The value to be contained in the successful result.
    /// </param>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing a successful operation with the specified value.
    /// </returns>
    public static implicit operator Result<T>(T value)
    {
        return Pass(value);
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="Result{T}"/> instance to an untyped <see cref="Result"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="Result{T}"/> instance to convert.
    /// </param>
    /// <returns>
    /// A <see cref="Result"/> instance representing the untyped result.
    /// </returns>
    public static implicit operator Result(Result<T> result)
    {
        return result.RemoveType();
    }
    
    /// <summary>
    /// Implicitly converts an <see cref="Result"/> instance to a <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="result">
    /// The <see cref="Result"/> instance to convert.
    /// </param>
    /// <returns>
    /// A <see cref="Result{T}"/> instance representing the typed result.
    /// </returns>
    public static implicit operator Result<T>(Result result)
    {
        return new Result<T>(result);
    }
}
using DDD.Core.Results;

namespace DDD.Core.Constants;

/// <summary>
/// Standard exit codes for operations.
/// Used for exiting applications or indicating operation results.
/// </summary>
public static class ExitCode
{
    /// <summary>
    /// Indicates a successful operation.
    /// </summary>
    public const int Success = 0;
    
    /// <summary>
    /// Indicates a failed operation.
    /// </summary>
    public const int Failure = 1;
    
    /// <summary>
    /// Converts a <see cref="Result{T}"/> or <see cref="Result"/> to an exit code asynchronously.
    /// </summary>
    /// <param name="resultTask">
    /// The task that represents the asynchronous operation returning a <see cref="Result{T}"/> or <see cref="Result"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the result value.
    /// </typeparam>
    /// <returns>
    /// An integer exit code: <see cref="Success"/> if the result is successful; otherwise, <see cref="Failure"/>.
    /// </returns>
    public static async Task<int> FromResultAsync<T>(Task<Result<T>> resultTask)
    {
        return FromBool((await resultTask).IsSuccessful);
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an exit code asynchronously.
    /// </summary>
    /// <param name="result">
    /// The task that represents the asynchronous operation returning a <see cref="Result"/>.
    /// </param>
    /// <returns>
    /// An integer exit code: <see cref="Success"/> if the result is successful; otherwise, <see cref="Failure"/>.
    /// </returns>
    public static async Task<int> FromResultAsync(Task<Result> result)
    {
        return FromBool((await result).IsSuccessful);
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an exit code.
    /// </summary>
    /// <param name="result">
    /// The result to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the result value.
    /// </typeparam>
    /// <returns>
    /// An integer exit code: <see cref="Success"/> if the result is successful; otherwise, <see cref="Failure"/>.
    /// </returns>
    public static int FromResult<T>(Result<T> result)
    {
        return FromBool(result.IsSuccessful);
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an exit code.
    /// </summary>
    /// <param name="result">
    /// The result to convert.
    /// </param>
    /// <returns>
    /// An integer exit code: <see cref="Success"/> if the result is successful; otherwise, <see cref="Failure"/>.
    /// </returns>
    public static int FromResult(Result result)
    {
        return FromBool(result.IsSuccessful);
    }

    /// <summary>
    /// Converts a boolean value to an exit code.
    /// </summary>
    /// <param name="isSuccess">
    /// The boolean value indicating success (true) or failure (false).
    /// </param>
    /// <returns>
    /// An integer exit code: <see cref="Success"/> if <paramref name="isSuccess"/> is true; otherwise, <see cref="Failure"/>.
    /// </returns>
    public static int FromBool(bool isSuccess)
    {
        return isSuccess ? Success : Failure;
    }
}
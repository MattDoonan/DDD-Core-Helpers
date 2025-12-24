namespace DDD.Core.Results.Extensions;

/// <summary>
/// Provides extension methods for converting <see cref="Result"/> and <see cref="Result{T}"/> instances to exit codes.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result"/> instance to an exit code.
    /// </summary>
    /// <param name="resultTask">
    /// The task containing the <see cref="Result"/> instance to convert.
    /// </param>
    /// <returns>
    /// An integer representing the exit code.
    /// </returns>
    public static async Task<int> ToExitCodeAsync(this Task<Result> resultTask)
    {
        return (await resultTask).ToExitCode();
    }
    
    /// <summary>
    /// Converts a <see cref="Result{T}"/> instance to an exit code.
    /// </summary>
    /// <param name="resultTask">
    /// The task containing the <see cref="Result{T}"/> instance to convert.
    /// </param>
    /// <typeparam name="T">
    /// The type of the value contained in the <see cref="Result{T}"/>.
    /// </typeparam>
    /// <returns>
    /// An integer representing the exit code.
    /// </returns>
    public static async Task<int> ToExitCodeAsync<T>(this Task<Result<T>> resultTask)
    {
        return (await resultTask).ToExitCode();
    }
}
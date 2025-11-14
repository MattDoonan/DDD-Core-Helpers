using DDD.Core.Results;

namespace DDD.Core.Constants;

public static class ExitCode
{
    public const int Success = 0;
    public const int Failure = 1;
    
    public static async Task<int> FromResultAsync<T>(Task<Result<T>> result)
    {
        return FromBool((await result).IsSuccessful);
    }

    public static async Task<int> FromResultAsync(Task<Result> result)
    {
        return FromBool((await result).IsSuccessful);
    }

    public static int FromResult<T>(Result<T> result)
    {
        return FromBool(result.IsSuccessful);
    }

    public static int FromResult(Result result)
    {
        return FromBool(result.IsSuccessful);
    }

    public static int FromBool(bool isSuccess)
    {
        return isSuccess ? Success : Failure;
    }
}
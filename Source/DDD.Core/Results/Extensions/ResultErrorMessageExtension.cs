namespace DDD.Core.Results.Extensions;

internal static class ResultErrorMessageExtension
{
    public static string ToResultMessage(this Exception exception)
    {
        return $"of an exception {exception.GetType().Name} with the message: {exception.Message}";
    }
}
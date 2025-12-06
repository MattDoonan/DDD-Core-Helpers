using DDD.Core.Results.Base;
using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Helpers;

public static class ResultErrorMessage
{
    public static string ExceptionToMessage(Exception exception)
    {
        return $"of an exception {exception.GetType().Name} with the message: {exception.Message}";
    }
}
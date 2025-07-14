using Outputs.Base;
using Outputs.Base.Interfaces;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Results;

public class ValueObjectResult : ResultStatus, IResultStatusBase<ValueObjectResult>
{
    private const string BaseErrorMessage = "Error during value object opperation";

    private ValueObjectResult(string successLog) : base(successLog)
    {
    }
    
    private ValueObjectResult(string baseMessage, string because) : base(baseMessage, because)
    {
    }

    public static ValueObjectResult Pass(string successLog = "")
    {
        return new ValueObjectResult(successLog);
    }
    
    public static ValueObjectResult<T> Pass<T>(T value, string successLog = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Pass(value, successLog);
    }

    public static ValueObjectResult Fail(string because = "")
    {
        return new ValueObjectResult(BaseErrorMessage, because);
    }
    
    public static ValueObjectResult<T> Fail<T>(string because = "")
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Fail(because);
    }

    public static ValueObjectResult AllPass(params ValueObjectResult[] result)
    {
        var succeeded = AllSucceeded(result.Cast<IResultStatus>().ToArray());
        return succeeded 
            ? new ValueObjectResult("All value objects results were successful")
            : new ValueObjectResult("Error checking value object results", "not all value object results were successful");
    }

    public static ValueObjectResult RemoveValue(IResultStatus status)
    {
        return status.IsFailure 
            ? new ValueObjectResult(BaseErrorMessage, status.ErrorReason)
            : new ValueObjectResult(status.SuccessLog); 
    }
}


public class ValueObjectResult<T> : ResultValue<T>, IResultValueBase<T, ValueObjectResult<T>> 
  where T : class, IValueObject
{
    private ValueObjectResult(T value, string successLog) : base(value, successLog)
    {
    }
    
    private ValueObjectResult(string because) : base($"Error returning {typeof(T).Name} value object", because)
    {
    }

    public static ValueObjectResult<T> Pass(T value, string successLog = "")
    {
        return new ValueObjectResult<T>(value, successLog);
    }

    public static ValueObjectResult<T> Fail(string because = "")
    {
        return new ValueObjectResult<T>(because);
    }
}
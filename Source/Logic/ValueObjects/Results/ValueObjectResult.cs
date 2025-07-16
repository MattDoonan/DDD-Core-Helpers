using Outputs.Base;
using Outputs.Base.Interfaces;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Results;

public class ValueObjectResult : BasicResult<ValueObjectResult>, IResultStatusBase<ValueObjectResult>
{
    private const string BaseErrorMessage = "Error during value object opperation";

    private ValueObjectResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
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

    public static ValueObjectResult RemoveValue(IResultStatus status)
    {
        return new ValueObjectResult(status);
    }
}


public class ValueObjectResult<T> : ResultValue<T>
  where T : class, IValueObject
{
    private ValueObjectResult(T value, string successLog) : base(value, successLog)
    {
    }
    
    private ValueObjectResult(string because) : base($"Error returning {typeof(T).Name} value object", because)
    {
    }

    internal static ValueObjectResult<T> Pass(T value, string successLog = "")
    {
        return new ValueObjectResult<T>(value, successLog);
    }

    internal static ValueObjectResult<T> Fail(string because = "")
    {
        return new ValueObjectResult<T>(because);
    }
}
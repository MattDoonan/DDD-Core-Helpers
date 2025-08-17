using Outputs.ObjectTypes;
using Outputs.Results;
using Outputs.Results.Abstract;
using Outputs.Results.Interfaces;

namespace ValueObjects.Results;

public class ValueObjectResult : BasicResult<ValueObjectResult>, IResultStatusBase<ValueObjectResult>
{

    private ValueObjectResult(IResultStatus resultStatus) : base(resultStatus)
    {
    }
    
    private ValueObjectResult(FailureType failureType, string because) : base(failureType, because)
    {
    }
    
    private ValueObjectResult()
    {
    }

    public static ValueObjectResult Pass()
    {
        return new ValueObjectResult();
    }
    
    public static ValueObjectResult<T> Pass<T>(T value)
        where T : class, IValueObject
    {
        return ValueObjectResult<T>.Pass(value);
    }
    
    public static ValueObjectResult Fail(string because = "")
    {
        return new ValueObjectResult(FailureType.ValueObject, because);
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


public class ValueObjectResult<T> : BasicValueResult<T, ValueObjectResult>
  where T : class, IValueObject
{
    private ValueObjectResult(T value) : base(value)
    {
    }
    
    private ValueObjectResult(FailureType failureType, string because) : base(failureType, because)
    {
    }

    internal static ValueObjectResult<T> Pass(T value)
    {
        return new ValueObjectResult<T>(value);
    }

    internal static ValueObjectResult<T> Fail(string because = "")
    {
        return new ValueObjectResult<T>(FailureType.ValueObject, because);
    }
    
    public static implicit operator ValueObjectResult<T>(T value)
    {
        return Pass(value);
    }
    
    public static implicit operator Result<T>(ValueObjectResult<T> result)
    {
        return Result.Create(result);
    }
    
}
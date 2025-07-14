using Core.ValueObjects.Types.Regular.Base;
using Outputs.Base;

namespace Core.Results;

public interface IValueObjectResult<T> : IResultValueBase<T, ValueObjectResult<T>> 
    where T : class, IValueObject;

public class ValueObjectResult<T> : ResultValueBase<T>, IValueObjectResult<T>
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
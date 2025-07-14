using Outputs.Base;

namespace Core.Results;

public interface IValueResult<T> : IResultValueBase<T, ValueResult<T>>;

public class ValueResult<T> :  ResultValueBase<T>, IValueResult<T>
{
    private ValueResult(T value, string successLog) : base(value, successLog)
    {
    }

    private ValueResult(string because) : base($"Error returning {typeof(T).Name}", because)
    {
    }

    public static ValueResult<T> Pass(T value, string successLog = "")
    {
        return new ValueResult<T>(value, successLog);
    }

    public static ValueResult<T> Fail(string because = "")
    {
        return new ValueResult<T>(because);
    }
}
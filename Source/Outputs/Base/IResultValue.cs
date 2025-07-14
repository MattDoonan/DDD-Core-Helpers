namespace Outputs.Base;

public interface IResultValue<out T> : IResultStatus
{
    public T Value { get; }
}


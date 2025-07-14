namespace Outputs.Base.Interfaces;

public interface IResultValue<out T> : IResultStatus
{
    public T Value { get; }
}


namespace Outputs.Results.Interfaces;

public interface IValueResult<out T> : IResultStatus
{
    public T Value { get; }
}


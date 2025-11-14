namespace DDD.Core.Interfaces.Values;

public interface ISingleValue<out TValue>
{
    public TValue Value { get; }
}
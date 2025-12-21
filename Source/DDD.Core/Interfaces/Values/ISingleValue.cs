namespace DDD.Core.Interfaces.Values;

/// <summary>
/// Interface for single value containers
/// Contains a single value of type <typeparamref name="TValue"/>
/// </summary>
/// <typeparam name="TValue">
/// The type of the value
/// </typeparam>
public interface ISingleValue<out TValue>
{
    public TValue Value { get; }
}
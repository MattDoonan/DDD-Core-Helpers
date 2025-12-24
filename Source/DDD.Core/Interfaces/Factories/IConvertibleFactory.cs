namespace DDD.Core.Interfaces.Factories;

/// <summary>
/// Factory interface for creating instances of <typeparamref name="T"/> from <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value used to create an instance of <typeparamref name="T"/>.
/// </typeparam>
/// <typeparam name="T">
/// The type of the object to be created.
/// </typeparam>
public interface IConvertibleFactory<in TValue, out T>
{
    static abstract T From(TValue value);
}
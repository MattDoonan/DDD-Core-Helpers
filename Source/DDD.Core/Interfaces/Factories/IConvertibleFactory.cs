namespace DDD.Core.Interfaces.Factories;

public interface IConvertibleFactory<in TValue, out T>
{
    static abstract T From(TValue value);
}
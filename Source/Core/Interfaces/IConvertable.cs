namespace Core.Interfaces;

public interface IConvertable<in TValue, out T>
{
    static abstract T From(TValue value);
}
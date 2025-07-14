using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Mappers;

public interface IValueToValueObjectMapper<in TValue, T>  
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
{
    ValueObjectResult<T> Map(TValue value);
}

public interface IValueObjectToValueMapper<TValue, in T>  
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
{
    MapperResult<TValue> Map(T value);
}

public interface IValueObjectMapper<TValue, T> : IValueToValueObjectMapper<TValue, T>, IValueObjectToValueMapper<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
{
}
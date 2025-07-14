using Core.Results;
using Core.ValueObjects.Types.Regular.Base;
using Core.ValueObjects.Types.Regular.Enums;

namespace Core.ValueObjects.Mappers;

public interface IEnumValueObjectMapper<out TEnum, TConvert, T> 
    where TEnum :  Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
{
    static abstract MapperResult<TConvert> Map(IEnumValueObject<TEnum, T> value);
    static abstract MapperResult<T> Map(TConvert value);
}
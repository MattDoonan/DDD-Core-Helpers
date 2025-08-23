using Outputs.Results.Basic;
using ValueObjects.Regular.Base;
using ValueObjects.Regular.Enums;

namespace ValueObjects.Mappers;

public interface IEnumValueObjectMapper<out TEnum, TConvert, T> 
    where TEnum :  Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
{
    static abstract MapperResult<TConvert> Map(IEnumValueObject<TEnum, TConvert, T> value);
    static abstract MapperResult<T> Map(TConvert value);
}
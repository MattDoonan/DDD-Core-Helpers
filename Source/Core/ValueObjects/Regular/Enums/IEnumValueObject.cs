using Base.Results.Basic;
using Base.ValueObjects.Regular.Base;

namespace Base.ValueObjects.Regular.Enums;

public interface IEnumValueObject<in TEnum, TConvert, T> : IValueObject
    where TEnum : Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
{
    static abstract MapperResult<T> Create(TConvert value);
    MapperResult<TConvert> Convert();
}
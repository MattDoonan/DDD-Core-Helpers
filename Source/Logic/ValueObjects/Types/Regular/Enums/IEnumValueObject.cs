using Outputs.ObjectTypes;
using ValueObjects.Results;
using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Regular.Enums;

public interface IEnumValueObject<in TEnum, TConvert, T> : IValueObject
    where TEnum : Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
{
    static abstract MapperResult<T> Create(TConvert value);
    MapperResult<TConvert> Convert();
}
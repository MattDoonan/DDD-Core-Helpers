using Base.Results.Basic;
using Base.ValueObjects.Mappers;
using Base.ValueObjects.Regular.Base;

namespace Base.ValueObjects.Regular.Enums;

public abstract class EnumValueObjectBase<TEnum, TConvert, T, TMapper>(TEnum value) : ValueObjectBase<TEnum>(value), IEnumValueObject<TEnum, TConvert, T>
    where TEnum : Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
    where TMapper : IEnumValueObjectMapper<TEnum, TConvert, T>, new()
{
    public static MapperResult<T> Create(TConvert value)
    {
        return TMapper.Map(value);
    }
    
    public MapperResult<TConvert> Convert()
    {
        return TMapper.Map(this);
    }
}
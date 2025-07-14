using Core.Results;
using Core.ValueObjects.Mappers;
using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Regular.Enums;

public abstract class EnumValueObjectBase<TEnum, TConvert, T, TMapper> : IEnumValueObject<TEnum, T>
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
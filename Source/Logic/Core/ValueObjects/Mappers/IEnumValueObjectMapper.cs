using Core.Results;
using Core.ValueObjects.Types.Regular.Base;
using Core.ValueObjects.Types.Regular.Cases;

namespace Core.ValueObjects.Mappers;

public interface IEnumValueObjectMapper<out TEnum, TConvert, T> 
    where TEnum :  Enum, IComparable<TEnum>, IEquatable<TEnum>
    where TConvert : IComparable<TConvert>, IEquatable<TConvert>
    where T : class, IValueObject<TEnum, T>
{
    static abstract ValueResult<TConvert> Map(IEnumValueObject<TEnum, T> value);
    static abstract ValueObjectResult<T> Map(TConvert value);
}
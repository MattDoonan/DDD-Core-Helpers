using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Regular.Enums;

public interface IEnumValueObject<in TEnum, T> : IValueObject
    where TEnum : Enum, IComparable<TEnum>, IEquatable<TEnum>
    where T : class, IValueObject<TEnum, T>
{

    
}
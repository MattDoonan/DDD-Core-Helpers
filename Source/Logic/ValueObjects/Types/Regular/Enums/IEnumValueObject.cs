using ValueObjects.Types.Regular.Base;

namespace ValueObjects.Types.Regular.Enums;

public interface IEnumValueObject<in TEnum, T> : IValueObject
    where TEnum : Enum, IComparable<TEnum>, IEquatable<TEnum>
    where T : class, IValueObject<TEnum, T>
{

    
}
namespace Core.ValueObjects.Regular.Base;

public interface ISingleValueObject<out TValue> : IValueObject
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    public TValue Value { get; }
}


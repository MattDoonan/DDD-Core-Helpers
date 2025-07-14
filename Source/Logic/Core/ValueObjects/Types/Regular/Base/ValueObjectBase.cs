namespace Core.ValueObjects.Types.Regular.Base;

public abstract class ValueObjectBase<TValue>(TValue value) : IValue<ValueObjectBase<TValue>>
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    public TValue Value { get; } = value;

    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }

    public virtual int CompareTo(object? obj)
    {
        if (obj is ValueObjectBase<TValue> otherValue)
        {
            return CompareTo(otherValue);
        }
        throw new ArgumentException($"Object is not of the correct type {typeof(TValue)}");
    }

    public virtual int CompareTo(ValueObjectBase<TValue>? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObjectBase<TValue> otherValue && Equals(otherValue);
    }

    public virtual bool Equals(ValueObjectBase<TValue>? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ValueObjectBase<TValue> left, ValueObjectBase<TValue> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObjectBase<TValue> left, ValueObjectBase<TValue> right)
    {
        return !(left == right);
    }
}
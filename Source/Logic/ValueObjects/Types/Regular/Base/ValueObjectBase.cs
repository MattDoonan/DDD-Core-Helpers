namespace ValueObjects.Types.Regular.Base;

public abstract class ValueObjectBase<TValue, T>(TValue value) : IValue<T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IValueObject<TValue, T>
{
    public TValue Value { get; } = value;

    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }

    public virtual int CompareTo(object? obj)
    {
        if (obj is T otherValue)
        {
            return CompareTo(otherValue);
        }
        throw new ArgumentException($"Object is not of the correct type {typeof(TValue)}");
    }

    public virtual int CompareTo(T? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is T otherValue && Equals(otherValue);
    }

    public virtual bool Equals(T? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ValueObjectBase<TValue, T> left, T right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObjectBase<TValue, T> left, T right)
    {
        return !(left == right);
    }
}
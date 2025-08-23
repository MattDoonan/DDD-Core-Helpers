namespace ValueObjects.Regular.Base;

public abstract class ValueObjectBase<TValue>(TValue value) : IValue<ValueObjectBase<TValue>>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
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
        if (obj is null)
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        return obj is ValueObjectBase<TValue> valueObjectBase 
               && Equals(valueObjectBase);
    }

    public virtual bool Equals(ValueObjectBase<TValue>? other)
    {
        if (other is null)
        {
            return false;
        }
        return GetType() == other.GetType() && Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ValueObjectBase<TValue> left, object right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObjectBase<TValue> left, object right)
    {
        return !(left == right);
    }
    
    public static bool operator <=(ValueObjectBase<TValue> a, ValueObjectBase<TValue> b)
    {
        return a.Value.CompareTo(b.Value) <= 0;
    }

    public static bool operator >=(ValueObjectBase<TValue> a, ValueObjectBase<TValue> b)
    {
        return a.Value.CompareTo(b.Value) >= 0;
    }
    
    public static bool operator <(ValueObjectBase<TValue> a, ValueObjectBase<TValue> b)
    {
        return a.Value.CompareTo(b.Value) < 0;
    }

    public static bool operator >(ValueObjectBase<TValue> a, ValueObjectBase<TValue> b)
    {
        return a.Value.CompareTo(b.Value) > 0;
    }
}
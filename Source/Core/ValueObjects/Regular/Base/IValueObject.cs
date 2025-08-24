namespace Base.ValueObjects.Regular.Base;

public interface IValueObject;

public interface IValueObject<TValue, T> : IValueObject, IValueObjectFactory<TValue, T>
    where T : class, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>;
namespace Core.ValueObjects.Types.Regular.Base;

public interface IValueObject;

public interface IValueObject<TValue, T> : IValueObject, IValue<T>, IValueObjectFactory<TValue, T>
    where T : class, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>;
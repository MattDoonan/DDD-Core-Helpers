using Base.ObjectTypes;

namespace Logic.ValueObjects.Regular.Base;

public interface IValueObject<TValue, T> : IValueObject, IValueObjectFactory<TValue, T>
    where T : class, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>;
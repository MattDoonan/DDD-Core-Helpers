using Outputs.ObjectTypes;

namespace ValueObjects.Types.Regular.Base;

public interface IValueObject<TValue, T> : IValueObject, IValue<T>, IValueObjectFactory<TValue, T>
    where T : class, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>;
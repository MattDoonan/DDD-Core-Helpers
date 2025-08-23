using Outputs.ObjectTypes;

namespace ValueObjects.Regular.Base;

public interface IValueObject<TValue, T> : IValueObject, IValueObjectFactory<TValue, T>
    where T : class, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>;
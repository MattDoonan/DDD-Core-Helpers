using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Identifiers.Base;

public interface IIdentifier : IValueObject;

public interface IIdentifier<out TValue> : IIdentifier, ISingleValueObject<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

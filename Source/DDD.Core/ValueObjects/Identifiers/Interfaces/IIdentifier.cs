using DDD.Core.ValueObjects.Base.Interfaces;
using DDD.Core.ValueObjects.SingleValueObjects.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Interfaces;

public interface IIdentifier : IValueObject;

public interface IIdentifier<out TValue> : IIdentifier, ISingleValueObject<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

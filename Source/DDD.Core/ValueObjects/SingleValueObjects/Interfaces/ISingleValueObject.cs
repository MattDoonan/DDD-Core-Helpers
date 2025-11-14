using DDD.Core.Interfaces.Values;
using DDD.Core.ValueObjects.Base.Interfaces;

namespace DDD.Core.ValueObjects.SingleValueObjects.Interfaces;

public interface ISingleValueObject<out TValue> : ISingleValue<TValue>, IValueObject
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

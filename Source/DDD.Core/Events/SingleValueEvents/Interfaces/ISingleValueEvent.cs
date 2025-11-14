using DDD.Core.Events.Base.Interfaces;
using DDD.Core.Interfaces.Values;

namespace DDD.Core.Events.SingleValueEvents.Interfaces;

public interface ISingleValueEvent<out TValue> : ISingleValue<TValue>, IEvent
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;
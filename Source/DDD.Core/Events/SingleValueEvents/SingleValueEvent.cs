using DDD.Core.Events.Base;
using DDD.Core.Events.SingleValueEvents.Interfaces;

namespace DDD.Core.Events.SingleValueEvents;

public abstract record SingleValueEvent<TValue>(TValue Value) : Event, ISingleValueEvent<TValue>
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
{
    
}
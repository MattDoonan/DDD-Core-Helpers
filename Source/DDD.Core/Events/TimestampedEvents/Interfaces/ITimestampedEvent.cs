using DDD.Core.Events.Base.Interfaces;

namespace DDD.Core.Events.TimestampedEvents.Interfaces;

public interface ITimestampedEvent : IEvent
{
    DateTime Timestamp { get; init; }
}
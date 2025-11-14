using DDD.Core.Events.Base;
using DDD.Core.Events.TimestampedEvents.Interfaces;

namespace DDD.Core.Events.TimestampedEvents;

public record TimestampedEvent : Event, ITimestampedEvent
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
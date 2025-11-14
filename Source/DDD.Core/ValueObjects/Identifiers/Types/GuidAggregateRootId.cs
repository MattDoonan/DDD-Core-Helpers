using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

public record GuidAggregateRootId<T>(Guid Value) : GuidIdentifier<T>(Value), IAggregateRootId<Guid>
    where T : GuidAggregateRootId<T>, IGuidFactory<T>
{
    
}
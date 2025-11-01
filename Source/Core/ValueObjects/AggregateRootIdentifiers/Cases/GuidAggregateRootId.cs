using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record GuidAggregateRootId<T>(Guid Value) : GuidIdentifier<T>(Value), IAggregateRootId<Guid>
    where T : GuidAggregateRootId<T>, IGuidFactory<T>
{
    
}
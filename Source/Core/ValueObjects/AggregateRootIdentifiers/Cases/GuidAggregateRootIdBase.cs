using Core.ValueObjects.Identifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record GuidAggregateRootIdBase<T>(Guid Value) : GuidIdentifierBase<T>(Value)
    where T : GuidAggregateRootIdBase<T>, IGuidFactory<T>
{
    
}
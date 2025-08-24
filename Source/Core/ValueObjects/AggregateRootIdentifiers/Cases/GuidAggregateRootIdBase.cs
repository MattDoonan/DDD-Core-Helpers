using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
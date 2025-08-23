using ValueObjects.AggregateRootIdentifiers.Base;
using ValueObjects.Identifiers.Cases;

namespace ValueObjects.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
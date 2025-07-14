using ValueObjects.Types.AggregateRootIdentifiers.Base;
using ValueObjects.Types.Identifiers.Cases;

namespace ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
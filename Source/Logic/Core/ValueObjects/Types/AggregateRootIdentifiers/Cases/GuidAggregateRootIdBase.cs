using Core.ValueObjects.Types.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Types.Identifiers.Base;
using Core.ValueObjects.Types.Identifiers.Cases;

namespace Core.ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
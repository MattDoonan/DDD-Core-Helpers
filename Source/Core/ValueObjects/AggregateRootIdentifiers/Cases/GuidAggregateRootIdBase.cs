using Base.ValueObjects.AggregateRootIdentifiers.Base;
using Base.ValueObjects.Identifiers.Cases;

namespace Base.ValueObjects.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
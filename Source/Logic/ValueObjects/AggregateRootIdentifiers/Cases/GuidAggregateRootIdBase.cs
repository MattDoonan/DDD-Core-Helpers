using Logic.ValueObjects.AggregateRootIdentifiers.Base;
using Logic.ValueObjects.Identifiers.Cases;

namespace Logic.ValueObjects.AggregateRootIdentifiers.Cases;

public class GuidAggregateRootIdBase<T>(Guid value) : GuidIdentifierBase<T>(value)
    where T : class, IAggregateRootId<Guid, T>
{
    
}
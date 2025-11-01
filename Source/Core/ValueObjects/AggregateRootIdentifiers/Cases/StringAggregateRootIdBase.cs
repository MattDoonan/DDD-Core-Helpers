using Core.Interfaces;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record StringAggregateRootIdBase<T>(string Value): StringIdentifier<T>(Value)
    where T : StringAggregateRootIdBase<T>, ISimpleValueObjectFactory<string, T>;
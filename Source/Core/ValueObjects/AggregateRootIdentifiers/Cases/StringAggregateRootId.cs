using Core.Interfaces;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record StringAggregateRootId<T>(string Value): StringIdentifier<T>(Value)
    where T : StringAggregateRootId<T>, ISimpleValueObjectFactory<string, T>;
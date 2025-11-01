using Core.Interfaces;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record StringAggregateRootId<T>(string Value): StringIdentifier<T>(Value), IAggregateRootId<string>
    where T : StringAggregateRootId<T>, ISimpleValueObjectFactory<string, T>;
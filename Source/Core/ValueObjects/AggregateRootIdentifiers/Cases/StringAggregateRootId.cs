using Core.Interfaces;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public record StringAggregateRootId<T>(string Value): StringIdentifier<T>(Value), IAggregateRootId<string>
    where T : StringAggregateRootId<T>, ISingleValueObjectFactory<string, T>;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.ValueObjects.Identifiers.Types;

public record StringAggregateRootId<T>(string Value): StringIdentifier<T>(Value), IAggregateRootId<string>
    where T : StringAggregateRootId<T>, ISingleValueObjectFactory<string, T>;
using Core.ValueObjects.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Identifiers.Cases;

namespace Core.ValueObjects.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
using ValueObjects.AggregateRootIdentifiers.Base;
using ValueObjects.Identifiers.Cases;

namespace ValueObjects.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
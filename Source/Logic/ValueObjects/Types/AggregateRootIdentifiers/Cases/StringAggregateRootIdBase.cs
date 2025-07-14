using ValueObjects.Types.AggregateRootIdentifiers.Base;
using ValueObjects.Types.Identifiers.Cases;

namespace ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
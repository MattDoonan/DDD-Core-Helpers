using Base.ValueObjects.AggregateRootIdentifiers.Base;
using Base.ValueObjects.Identifiers.Cases;

namespace Base.ValueObjects.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
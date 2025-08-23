using Logic.ValueObjects.AggregateRootIdentifiers.Base;
using Logic.ValueObjects.Identifiers.Cases;

namespace Logic.ValueObjects.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
using Core.ValueObjects.Types.AggregateRootIdentifiers.Base;
using Core.ValueObjects.Types.Identifiers.Cases;

namespace Core.ValueObjects.Types.AggregateRootIdentifiers.Cases;

public class StringAggregateRootIdBase<T>(string value): StringIdentifierBase<T>(value)
    where T : class, IAggregateRootId<string, T>;
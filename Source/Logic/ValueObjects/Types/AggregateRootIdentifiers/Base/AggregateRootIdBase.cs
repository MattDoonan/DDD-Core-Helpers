using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.AggregateRootIdentifiers.Base;

public class AggregateRootIdBase<TValue>(TValue value) : IdentifierBase<TValue>(value)
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

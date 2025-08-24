using Base.ValueObjects.Identifiers.Base;

namespace Base.ValueObjects.AggregateRootIdentifiers.Base;

public class AggregateRootIdBase<TValue>(TValue value) : IdentifierBase<TValue>(value)
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

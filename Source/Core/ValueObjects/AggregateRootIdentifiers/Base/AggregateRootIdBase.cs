using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.AggregateRootIdentifiers.Base;

public class AggregateRootIdBase<TValue>(TValue value) : IdentifierBase<TValue>(value)
    where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>;

namespace Base.ValueObjects.Regular.Base;

public interface IValue<T> : IComparable, IComparable<T>, IEquatable<T>;
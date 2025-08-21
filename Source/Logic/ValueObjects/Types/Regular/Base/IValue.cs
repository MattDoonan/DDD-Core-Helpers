namespace ValueObjects.Types.Regular.Base;

public interface IValue<T> : IComparable, IComparable<T>, IEquatable<T>;
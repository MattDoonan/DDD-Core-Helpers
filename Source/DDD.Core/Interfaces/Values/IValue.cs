namespace DDD.Core.Interfaces.Values;

public interface IValue<T> : IComparable, IComparable<T>, IEquatable<T>;
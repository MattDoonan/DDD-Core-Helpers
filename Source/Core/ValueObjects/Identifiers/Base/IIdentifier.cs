using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Identifiers.Base;

public interface IIdentifier : IValueObject;

public interface IGuiIdentifier<T>
    where T : class, IIdentifier
{
    static abstract ValueObjectResult<T> Create();
    static abstract ValueObjectResult<T> Create(string value);
}

public interface IIdentifier<TValue, T> : IIdentifier, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>;
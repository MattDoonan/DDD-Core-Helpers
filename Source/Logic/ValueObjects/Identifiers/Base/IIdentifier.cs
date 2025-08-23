using Outputs.ObjectTypes;
using Outputs.Results;
using Outputs.Results.Basic;
using ValueObjects.Regular.Base;

namespace ValueObjects.Identifiers.Base;

public interface IGuiIdentifier<T>
    where T : class, IIdentifier
{
    ValueObjectResult<T> Create();
    ValueObjectResult<T> Create(string value);
}

public interface IIdentifier<TValue, T> : IIdentifier, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>;
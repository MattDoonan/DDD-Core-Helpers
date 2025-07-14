using Core.Results;
using Core.ValueObjects.Types.Identifiers.Lists;
using Core.ValueObjects.Types.Regular.Base;

namespace Core.ValueObjects.Types.Identifiers.Base;

public interface IIdentifier : IValueObject;

public interface IIdentifierCommands<out TValue, T> : IIdentifier
    where T : class, IIdentifier
{
    bool IsInList(IIdentifierList<TValue, T> identifierList); 
}

public interface IGuiIdentifier<T> : IIdentifierCommands<Guid, T>
    where T : class, IIdentifier
{
    ValueObjectResult<T> Create();
    ValueObjectResult<T> Create(string value);
}

public interface IIdentifier<TValue, T> : IIdentifierCommands<TValue, T>, IValueObject<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>;
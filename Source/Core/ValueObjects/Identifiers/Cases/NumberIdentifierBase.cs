using System.Numerics;
using Base.ValueObjects.Identifiers.Base;
using Base.ValueObjects.Identifiers.Lists;
using Base.ValueObjects.Regular.Numbers;

namespace Base.ValueObjects.Identifiers.Cases;

public class NumberIdentifierBase<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIdentifier
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}
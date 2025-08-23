using System.Numerics;
using Base.ObjectTypes;
using Logic.ValueObjects.Identifiers.Base;
using Logic.ValueObjects.Identifiers.Lists;
using Logic.ValueObjects.Regular.Numbers;

namespace Logic.ValueObjects.Identifiers.Cases;

public class NumberIdentifierBase<TValue, T>(TValue value) : NumberValueObjectBase<TValue, T>(value), IIdentifier
    where TValue : INumber<TValue>, IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>

{
    public bool IsInList(IIdentifierList<NumberIdentifierBase<TValue, T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}
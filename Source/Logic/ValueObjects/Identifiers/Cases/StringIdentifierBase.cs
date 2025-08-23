using Base.ObjectTypes;
using Logic.ValueObjects.Identifiers.Base;
using Logic.ValueObjects.Identifiers.Lists;
using Logic.ValueObjects.Regular.Strings;

namespace Logic.ValueObjects.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifier
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifierBase<T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}
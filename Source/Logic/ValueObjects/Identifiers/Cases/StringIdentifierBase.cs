using Outputs.ObjectTypes;
using ValueObjects.Identifiers.Base;
using ValueObjects.Identifiers.Lists;
using ValueObjects.Regular.Strings;

namespace ValueObjects.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifier
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifierBase<T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}
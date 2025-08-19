using ValueObjects.Types.Identifiers.Base;
using ValueObjects.Types.Identifiers.Lists;
using ValueObjects.Types.Regular.Strings;

namespace ValueObjects.Types.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifier
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<StringIdentifierBase<T>> identifierList)
    {
        return identifierList.Get(this).IsSuccessful;
    }
}
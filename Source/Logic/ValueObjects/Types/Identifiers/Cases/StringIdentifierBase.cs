using ValueObjects.Types.Identifiers.Base;
using ValueObjects.Types.Identifiers.Lists;
using ValueObjects.Types.Regular.Strings;

namespace ValueObjects.Types.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifierCommands<string, T>
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<string, T> identifierList)
    {
        return identifierList.Get(Value).Successful;
    }
}
using Core.ValueObjects.Types.Identifiers.Base;
using Core.ValueObjects.Types.Identifiers.Lists;
using Core.ValueObjects.Types.Regular.Strings;

namespace Core.ValueObjects.Types.Identifiers.Cases;

public class StringIdentifierBase<T>(string value): StringValueObjectBase<T>(value), IIdentifierCommands<string, T>
    where T : class, IIdentifier<string, T>
{
    public bool IsInList(IIdentifierList<string, T> identifierList)
    {
        return identifierList.Get(Value).Successful;
    }
}
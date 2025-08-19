using Outputs.Results;
using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.Identifiers.Lists;

public interface IIdentifierList<in TValue, T> 
    where T : class, IIdentifier
{
    List<T> Values { get; }
    Result Add(T identifier);
    Result Add(TValue identifier);

    Result Remove(T identifier);
    Result<T> Get(T identifier);
    Result<T> Get(TValue identifier);
    void OrderAsc();
    void OrderDesc();

}
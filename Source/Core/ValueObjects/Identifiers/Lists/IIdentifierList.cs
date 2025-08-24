using Base.Results.Advanced;
using Base.ValueObjects.Identifiers.Base;

namespace Base.ValueObjects.Identifiers.Lists;

public interface IIdentifierList<T> 
    where T : IIdentifier
{
    List<T> Values { get; }
    Result Add(T identifier);
    Result Remove(T identifier);
    Result<T> Get(T identifier);
    void OrderAsc();
    void OrderDesc();

}
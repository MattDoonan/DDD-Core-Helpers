using Core.Results;
using Core.ValueObjects.Types.Identifiers.Base;

namespace Core.ValueObjects.Types.Identifiers.Lists;

public interface IIdentifierList<in TValue, T> 
    where T : class, IIdentifier
{
    List<T> Values { get; }
    EntityResult Add(T identifier);
    EntityResult Remove(T identifier);
    ValueObjectResult<T> Get(T identifier);
    ValueObjectResult<T> Get(TValue identifier);
    void OrderAsc();
    void OrderDesc();

}
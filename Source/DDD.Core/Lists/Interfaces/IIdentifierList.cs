using DDD.Core.Results;
using DDD.Core.ValueObjects.Identifiers.Interfaces;

namespace DDD.Core.Lists.Interfaces;

public interface IIdentifierList<T> 
    where T : IIdentifier
{
    IReadOnlyCollection<T> Values { get; }
    EntityResult Add(T identifier);
    EntityResult Remove(T identifier);
    void Clear();
    List<T> Copy();

}
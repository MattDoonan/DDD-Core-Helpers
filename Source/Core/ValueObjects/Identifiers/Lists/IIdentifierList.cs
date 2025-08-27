using Core.Results.Advanced;
using Core.Results.Basic;
using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.Identifiers.Lists;

public interface IIdentifierList<T> 
    where T : IIdentifier
{
    IReadOnlyCollection<T> Values { get; }
    EntityResult Add(T identifier);
    EntityResult Remove(T identifier);
    void Clear();
    List<T> Copy();

}
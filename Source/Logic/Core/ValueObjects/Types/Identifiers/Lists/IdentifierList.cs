using Core.Results;
using Core.ValueObjects.Types.Identifiers.Base;

namespace Core.ValueObjects.Types.Identifiers.Lists;

public class IdentifierList<TValue, T> : IIdentifierList<TValue, T> 
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>
{
    public List<T> Values { get; }

    public IdentifierList()
    {
        Values = [];
    }
    
    public IdentifierList(List<T> values)
    {
        Values = values;
    }

    public EntityResult Add(T identifier)
    {
        var contains = Get(identifier);
        if (contains.Successful)
        {
            return EntityResult.Fail("the identifier already exists in the list");
        }
        Values.Add(identifier);
        return EntityResult.Pass();
    }

    public EntityResult Remove(T identifier)
    {
       var removed = Values.Remove(identifier); 
       return removed ? EntityResult.Pass() : EntityResult.Fail("the identifier does not exist in the list");
    }

    public ValueObjectResult<T> Get(T identifier)
    {
        return Get(identifier.Value);
    }
    
    public ValueObjectResult<T> Get(TValue value)
    {
        var existingItem = Values.FirstOrDefault(id => id.Value.Equals(value));
        return existingItem == null
            ? ValueObjectResult<T>.Fail("the identifier does not exist in the list")
            : ValueObjectResult<T>.Pass(existingItem);
    }

    public void OrderAsc()
    {
        Values.Sort();
    }

    public void OrderDesc()
    {
        Values.Reverse();
    }
}
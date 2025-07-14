using ValueObjects.Results;
using ValueObjects.Types.Identifiers.Base;

namespace ValueObjects.Types.Identifiers.Lists;

public class IdentifierList<TValue, T>(List<T> values) : IIdentifierList<TValue, T>
    where TValue : IComparable<TValue>, IEquatable<TValue>
    where T : class, IIdentifier<TValue, T>
{
    public List<T> Values { get; } = values;

    public IdentifierList() : this([])
    {
    }

    public ValueObjectResult Add(T identifier)
    {
        var contains = Get(identifier);
        if (contains.Successful)
        {
            return ValueObjectResult.Fail("the identifier already exists in the list");
        }
        Values.Add(identifier);
        return ValueObjectResult.Pass($"Successfully added the identifier of type {typeof(T).Name}");
    }

    public ValueObjectResult Remove(T identifier)
    {
       var removed = Values.Remove(identifier); 
       return removed 
           ? ValueObjectResult.Pass($"Successfully removed {typeof(T)} from list") 
           : ValueObjectResult.Fail("the identifier does not exist in the list");
    }

    public ValueObjectResult<T> Get(T identifier)
    {
        return Get(identifier.Value);
    }
    
    public ValueObjectResult<T> Get(TValue value)
    {
        var existingItem = Values.FirstOrDefault(id => id.Value.Equals(value));
        return existingItem == null
            ? ValueObjectResult.Fail<T>("the identifier does not exist in the list")
            : ValueObjectResult.Pass(existingItem);
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
using Core.Results.Advanced;
using Core.Results.Basic;
using Core.ValueObjects.Identifiers.Base;

namespace Core.ValueObjects.Identifiers.Lists;

public class IdentifierList<T>(params T[] values) : IIdentifierList<T>
    where T : class, IIdentifier
{
    private readonly List<T> _values = values.ToList();
    public IReadOnlyCollection<T> Values => _values.AsReadOnly();

    public IdentifierList() : this([])
    {
    }

    public EntityResult Add(T identifier)
    {
        var contains = Get(identifier);
        if (contains.IsSuccessful)
        {
            return EntityResult.Fail("the identifier already exists in the list");
        }
        _values.Add(identifier);
        return EntityResult.Pass();
    }

    public EntityResult Remove(T identifier)
    {
       var removed = _values.Remove(identifier); 
       return removed 
           ? EntityResult.Pass() 
           : EntityResult.Fail("the identifier does not exist in the list");
    }

    public ValueObjectResult<T> Get(T identifier)
    {
        var existingItem = _values.FirstOrDefault(id => id.Equals(identifier));
        return existingItem == null
            ? ValueObjectResult.Fail<T>("the identifier does not exist in the list")
            : ValueObjectResult.Pass(existingItem);
    }

    public void Clear()
    {
        _values.Clear();
    }

    public List<T> Copy()
    {
        return _values.ToList();
    }
    
    public static implicit operator IdentifierList<T>(T list)
    {
        return new IdentifierList<T>(list);
    }
    
    public static implicit operator List<T>(IdentifierList<T> list)
    {
        return list.Values.ToList();
    } 
}
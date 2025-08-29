using Core.Entities.Regular;
using Core.Results.Basic;

namespace Core.Entities.List;

public class EntityList<T>(params T[] values) : IEntityList<T>
    where T : class, IEntity
{
    private readonly List<T> _entities = values.ToList();
    
    public IReadOnlyCollection<T> Entities => _entities.AsReadOnly();

    public EntityList() : this ([])
    {
    }
    
    public EntityResult Add(T identifier)
    {
        if (_entities.Contains(identifier))
        {
            return EntityResult.Fail("entity already exists in the list");
        }
        _entities.Add(identifier);
        return EntityResult.Pass();
    }

    public EntityResult Remove(T identifier)
    {
        if (!_entities.Contains(identifier))
        {
            return EntityResult.Fail("entity does not exist in the list");
        }
        _entities.Remove(identifier);
        return EntityResult.Pass();
    }

    public void Clear()
    {
        _entities.Clear();
    }

    public List<T> Copy()
    {
        return _entities.ToList();
    }
    
    public static implicit operator EntityList<T>(T list)
    {
        return new EntityList<T>(list);
    }
    
    public static implicit operator List<T>(EntityList<T> list)
    {
        return list.Entities.ToList();
    } 
}
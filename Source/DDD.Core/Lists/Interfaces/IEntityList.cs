using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;

namespace DDD.Core.Lists.Interfaces;

public interface IEntityList<T> 
    where T : IEntity
{
    IReadOnlyCollection<T> Entities { get; }
    EntityResult Add(T identifier);
    EntityResult Remove(T identifier);
    void Clear();
    List<T> Copy();
}
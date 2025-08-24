using Core.Entities.Regular;
using Core.Results.Basic;

namespace Core.Entities.List;

public interface IEntityList<T> 
    where T : IEntity
{
    IReadOnlyCollection<T> Entities { get; }
    EntityResult Add(T identifier);
    EntityResult Remove(T identifier);
    EntityResult<T> Get(T identifier);
    void Clear();
    List<T> Copy();
}
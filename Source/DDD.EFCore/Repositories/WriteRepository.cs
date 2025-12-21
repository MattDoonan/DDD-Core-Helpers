using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Repositories;

public class WriteRepository<T> : ISingleRepository<T>
    where T : Entity, IAggregateRoot
{
     private readonly DbSet<T> _dbSet;
    
    public WriteRepository(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
    }
    
    public void Add(T aggregateRoot)
    {
        _dbSet.Add(aggregateRoot);
    }
    
    public void AddMany(IEnumerable<T> aggregateRoots)
    {
        _dbSet.AddRange(aggregateRoots);

    }

    public void Update(T aggregateRoot)
    {
        _dbSet.Update(aggregateRoot);
    }

    public void UpdateMany(IEnumerable<T> aggregateRoots)
    {
        _dbSet.UpdateRange(aggregateRoots);
    }

    public void Remove(T aggregateRoot)
    {
        _dbSet.Remove(aggregateRoot);
    }

    protected void Attach(T aggregateRoot)
    {
        var entry = _dbSet.Entry(aggregateRoot);
        if (entry.State is not EntityState.Detached)
        {
            return;
        }
        _dbSet.Attach(aggregateRoot);
        entry.State = EntityState.Modified;
    }
}
using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Repositories;

/// <summary>
/// A repository for managing write operations on aggregate roots.
/// Provides methods for adding, updating, and removing aggregate roots.
/// </summary>
/// <typeparam name="T">
/// The type of the aggregate root.
/// </typeparam>
public class WriteRepository<T> : ISingleRepository<T>
    where T : Entity, IAggregateRoot
{
     private readonly DbSet<T> _dbSet;
    
    public WriteRepository(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
    }
    
    /// <summary>
    /// Adds a new aggregate root to the database context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to add.
    /// </param>
    public void Add(T aggregateRoot)
    {
        _dbSet.Add(aggregateRoot);
    }
    
    /// <summary>
    /// Adds multiple aggregate roots to the database context.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to add.
    /// </param>
    public void AddMany(IEnumerable<T> aggregateRoots)
    {
        _dbSet.AddRange(aggregateRoots);

    }
    
    /// <summary>
    /// Updates an existing aggregate root in the database context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to update.
    /// </param>
    public void Update(T aggregateRoot)
    {
        _dbSet.Update(aggregateRoot);
    }

    /// <summary>
    /// Updates multiple aggregate roots in the database context.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to update.
    /// </param>
    public void UpdateMany(IEnumerable<T> aggregateRoots)
    {
        _dbSet.UpdateRange(aggregateRoots);
    }
    
    /// <summary>
    /// Removes an aggregate root from the database context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to remove.
    /// </param>
    public void Remove(T aggregateRoot)
    {
        _dbSet.Remove(aggregateRoot);
    }
    
    /// <summary>
    /// Attaches an aggregate root to the database context and marks it as modified.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to attach.
    /// </param>
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
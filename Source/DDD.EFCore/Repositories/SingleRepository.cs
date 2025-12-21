
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Repositories;

/// <summary>
/// A repository for managing single aggregate roots.
/// Provides methods for adding, updating, and removing aggregate roots.
/// Inherits from QueryRepository to support querying capabilities.
/// </summary>
/// <typeparam name="TAggregate">
/// The type of the aggregate root.
/// </typeparam>
public class SingleRepository<TAggregate> : QueryRepository<TAggregate>, ISingleRepository<TAggregate>
    where TAggregate : class
{
    private readonly DbSet<TAggregate> _dbSet;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleRepository{TAggregate}"/> class.
    /// </summary>
    /// <param name="dbSet">
    /// The DbSet for the aggregate root.
    /// </param>
    public SingleRepository(DbSet<TAggregate> dbSet)
    {
        _dbSet = dbSet;
    }
    
    /// <summary>
    /// Adds a new aggregate root to the repository.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to add.
    /// </param>
    public void Add(TAggregate aggregateRoot)
    {
        _dbSet.Add(aggregateRoot);
    }
    
    /// <summary>
    /// Adds multiple aggregate roots to the repository.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to add.
    /// </param>
    public void AddMany(IEnumerable<TAggregate> aggregateRoots)
    {
        _dbSet.AddRange(aggregateRoots);
    }
    
    /// <summary>
    /// Updates an existing aggregate root in the repository.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to update.
    /// </param>
    public void Update(TAggregate aggregateRoot)
    {
        _dbSet.Update(aggregateRoot);
    }
    
    /// <summary>
    /// Updates multiple aggregate roots in the repository.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to update.
    /// </param>
    public void UpdateMany(IEnumerable<TAggregate> aggregateRoots)
    {
        _dbSet.UpdateRange(aggregateRoots);
    }

    /// <summary>
    /// Removes an aggregate root from the repository.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to remove.
    /// </param>
    public void Remove(TAggregate aggregateRoot)
    {
        _dbSet.Remove(aggregateRoot);
    }
    
    /// <summary>
    /// Attaches an aggregate root to the repository and marks it as modified.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to attach.
    /// </param>
    protected void Attach(TAggregate aggregateRoot)
    {
        var entry = _dbSet.Entry(aggregateRoot);
        if (entry.State is not EntityState.Detached)
        {
            return;
        }
        _dbSet.Attach(aggregateRoot);
        entry.State = EntityState.Modified;
    }
    
    /// <summary>
    /// Gets the queryable for the aggregate root.
    /// Override this method to customize the queryable
    /// and to include related entities as needed.
    /// </summary>
    /// <returns>
    /// The queryable for the aggregate root.
    /// </returns>
    protected override IQueryable<TAggregate> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}
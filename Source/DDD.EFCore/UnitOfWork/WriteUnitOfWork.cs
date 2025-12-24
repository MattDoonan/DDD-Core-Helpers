using DDD.Core.Contexts;

namespace DDD.Core.UnitOfWork;

/// <summary>
/// Unit of Work for write operations using a specific DbContext.
/// Provides methods to add and update aggregate roots.
/// </summary>
/// <typeparam name="TDbContext">
/// The type of DbContext used by this Unit of Work.
/// </typeparam>
public class WriteUnitOfWork<TDbContext> : DbContextWrapper<TDbContext>
    where TDbContext : DbContext
{

    public WriteUnitOfWork(TDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Adds a new aggregate root to the context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of the aggregate root.
    /// </typeparam>
    public void Add<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Add(aggregateRoot);
    }
    
    /// <summary>
    /// Adds multiple aggregate roots to the context.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of the aggregate roots.
    /// </typeparam>
    public void AddMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.AddRange(aggregateRoots);
    }

    /// <summary>
    /// Updates an existing aggregate root in the context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to update.
    /// </param>
    /// <typeparam name="T">
    /// The type of the aggregate root.
    /// </typeparam>
    public void Update<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Update(aggregateRoot);
    }

    /// <summary>
    /// Updates multiple existing aggregate roots in the context.
    /// </summary>
    /// <param name="aggregateRoots">
    /// The aggregate roots to update.
    /// </param>
    /// <typeparam name="T">
    /// The type of the aggregate roots.
    /// </typeparam>
    public void UpdateMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.UpdateRange(aggregateRoots);
    }

    /// <summary>
    /// Removes an aggregate root from the context.
    /// </summary>
    /// <param name="aggregateRoot">
    /// The aggregate root to remove.
    /// </param>
    /// <typeparam name="T">
    /// The type of the aggregate root.
    /// </typeparam>
    public void Remove<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Remove(aggregateRoot);
    }
    
}
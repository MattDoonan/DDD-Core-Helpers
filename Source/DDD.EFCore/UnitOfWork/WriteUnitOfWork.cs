using DDD.Core.Extensions;
using DDD.Core.Results;

namespace DDD.Core.UnitOfWork;

/// <summary>
/// Unit of Work for write operations using a specific DbContext.
/// Provides methods to add and update aggregate roots.
/// </summary>
/// <typeparam name="TDbContext">
/// The type of DbContext used by this Unit of Work.
/// </typeparam>
public class WriteUnitOfWork<TDbContext> : PersistUnitOfWork<TDbContext>
    where TDbContext : DbContext
{

    public WriteUnitOfWork(TDbContext context) : base(context)
    {
    }
    
    public void Add<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Add(aggregateRoot);
    }
    
    public void AddMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.AddRange(aggregateRoots);
    }

    public void Update<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Update(aggregateRoot);
    }

    public void UpdateMany<T>(IEnumerable<T> aggregateRoots)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.UpdateRange(aggregateRoots);
    }

    public void Remove<T>(T aggregateRoot)
        where T : class
    {
        var dbSet = Context.Set<T>();
        dbSet.Remove(aggregateRoot);
    }
    
}
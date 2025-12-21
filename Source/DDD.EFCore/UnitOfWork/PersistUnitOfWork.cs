using DDD.Core.Extensions;
using DDD.Core.Results;

namespace DDD.Core.UnitOfWork;

/// <summary>
/// Unit of Work for persist operations using a specific DbContext.
/// </summary>
/// <typeparam name="TDbContext">
/// The type of DbContext used by this Unit of Work.
/// </typeparam>
public class PersistUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    
    protected readonly TDbContext Context;

    public PersistUnitOfWork(TDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Saves all changes made in this unit of work to the underlying database.
    /// </summary>
    /// <param name="token">
    /// A cancellation token that can be used to cancel the save operation.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains a <see cref="RepoResult"/>
    /// indicating the success or failure of the operation.
    /// </returns>
    public Task<RepoResult> SaveChangesAsync(CancellationToken token = default)
    {
        return Context.SaveAsync(token);
    }
    
}
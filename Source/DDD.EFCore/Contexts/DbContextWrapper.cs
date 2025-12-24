using DDD.Core.Extensions;
using DDD.Core.Results;

namespace DDD.Core.Contexts;

/// <summary>
/// Base class for DbContext to wrap common operations.
/// Uses RepoResult for operation results to catch exceptions.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class DbContextWrapper<TDbContext>
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    public DbContextWrapper(TDbContext context)
    {
        Context = context;
    }
    
    /// <summary>
    /// Saves changes to the database and returns a RepoResult indicating success or failure.
    /// </summary>
    /// <param name="token">
    /// A CancellationToken to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A Task representing the asynchronous operation, containing a RepoResult.
    /// </returns>
    public Task<RepoResult> SaveChangesAsync(CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
        {
            return Task.FromResult(
                RepoResult.OperationCancelled());
        }
        return Context.SaveChangesAsync(token)
            .ToRepoResultAsync();
    }
    
    
    /// <summary>
    /// Saves changes to the database and returns a RepoResult indicating success or failure.
    /// </summary>
    /// <returns>
    /// A RepoResult indicating the outcome of the save operation.
    /// </returns>
    public RepoResult SaveChanges()
    {
        try
        {
            Context.SaveChanges();
            return RepoResult.Pass();
        }
        catch (Exception e)
        {
            return e.ToRepoResult();
        }
    }
    
    
    
}
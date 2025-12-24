using DDD.Core.Contexts;
using DDD.Core.Factories;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.UnitOfWork;

/// <summary>
/// Base class for Unit of Work contexts using a specific DbContext.
/// Provides lazy initialization and caching of repositories.
/// <remarks>
///     <para>
///         A <see cref="UnitOfWorkContext{TDbContext}"/> is responsible for managing the lifecycle of repositories
///         associated with a specific DbContext type. It ensures that each repository is created only once
///         and reused throughout the lifetime of the Unit of Work context.
///     </para>
///     <para>
///         To use this class, derive a concrete Unit of Work context class from it, specifying the DbContext type.
///         Implement properties or methods to access the required repositories using the <see cref="LazyGet{TRepository}"/> method.
///         This method handles the lazy creation and caching of repository instances.
///     </para>
/// </remarks>
/// </summary>
/// <typeparam name="TDbContext">
/// The type of DbContext used by this Unit of Work context.
/// </typeparam>
public class UnitOfWorkContext<TDbContext> : DbContextWrapper<TDbContext>
    where TDbContext : DbContext
{
    private readonly RepositoryCache _repositoryCache = new();
    
    protected UnitOfWorkContext(TDbContext dbContext) : base(dbContext)
    {
    }
    
    /// <summary>
    /// Lazily gets or creates a repository of the specified type.
    /// The Repository constructor must only require the DbContext as a parameter or a DbSet of the DbContext.
    /// <remarks>
    ///     <para>
    ///         Use this method within your derived Unit of Work context to access repositories.
    ///         It ensures that each repository is created only once and cached for future use.
    ///         For example:
    ///         public IRepository Repository => LazyGet<Repository>();
    ///     </para>
    /// </remarks>
    /// </summary>
    /// <typeparam name="TRepository">
    /// The type of repository to get or create.
    /// </typeparam>
    /// <returns>
    /// The repository instance of the specified type.
    /// </returns>
    protected TRepository LazyGet<TRepository>() 
        where TRepository : class, ISingleRepository
    {
        var cachedRepositoryResult = _repositoryCache.Get<TRepository>();
        return cachedRepositoryResult.IsSuccessful 
            ? cachedRepositoryResult.Output 
            : CreateAndCacheRepository<TRepository>();
    }
    
    
    /// <summary>
    /// Removes a repository of the specified type from the cache.
    /// </summary>
    /// <typeparam name="TRepository">
    /// The type of repository to remove.
    /// </typeparam>
    /// <returns>
    /// True if the repository was successfully removed; otherwise, false.
    /// </returns>
    public bool RemoveFromCache<TRepository>() 
        where TRepository : class, ISingleRepository
    {
        return _repositoryCache.Remove<TRepository>();
    }
    
    /// <summary>
    /// Clears all repositories from the cache.
    /// </summary>
    public void ClearCache()
    {
        _repositoryCache.Clear();
    }
    
    private TRepository CreateAndCacheRepository<TRepository>() 
        where TRepository : class, ISingleRepository
    {
        var repository = RepositoryInstanceFactory.Create<TDbContext, TRepository>(Context);
        _repositoryCache.Add(repository);
        return repository;
    }
}
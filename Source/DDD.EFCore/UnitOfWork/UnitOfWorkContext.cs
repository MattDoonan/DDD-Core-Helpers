using DDD.Core.Extensions;
using DDD.Core.Factories;
using DDD.Core.Results;
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
public class UnitOfWorkContext<TDbContext> : PersistUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    private readonly Dictionary<Type, object> _singleUowCache = new();
    
    protected UnitOfWorkContext(TDbContext dbContext) : base(dbContext)
    {
    }
    
    /// <summary>
    /// Lazily gets or creates a repository of the specified type.
    /// The Repository constructor must only require the DbContext as a parameter or a DbSet of the DbContext.
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
        var type = typeof(TRepository);
        if (_singleUowCache.TryGetValue(type, out var instance) )
        {
            return (TRepository)instance;
        }
        return CreateAndStoreRepository<TRepository>();
    }
    
    private TRepository CreateAndStoreRepository<TRepository>() 
        where TRepository : class, ISingleRepository
    {
        var repository = RepositoryFactory.Create<TDbContext, TRepository>(Context);
        StoreRepositoryInstance(repository);
        return repository;
    }
    
    private void StoreRepositoryInstance<TRepo>(TRepo repository) 
        where TRepo : class, ISingleRepository
    {
        var type = typeof(TRepo);
        _singleUowCache[type] = repository;
    }
}
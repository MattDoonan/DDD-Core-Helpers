using DDD.Core.Results;
using DDD.Core.UnitOfWork.Exceptions;

namespace DDD.Core.UnitOfWork;

/// <summary>
/// Caches repository instances for reuse within a Unit of Work context.
/// </summary>
public class RepositoryCache
{
    private readonly Dictionary<Type, object> _repositoryCache = new();
    
    /// <summary>
    /// Gets a cached repository of the specified type.
    /// </summary>
    /// <typeparam name="TRepository">
    /// The type of repository to get.
    /// </typeparam>
    /// <returns></returns>
    /// <exception cref="RepositoryCacheException">
    /// Thrown when the cached repository type does not match the requested type.
    /// </exception>
    public InfraResult<TRepository> Get<TRepository>() 
        where TRepository : class
    {
        var repositoryResult = Get(typeof(TRepository));
        if (repositoryResult.IsFailure)
        {
            return repositoryResult.ToTypedInfraResult<TRepository>();
        }
        return repositoryResult.Output is TRepository repository
            ? InfraResult.Pass(repository)
            : throw new RepositoryCacheException("the cached repository type does not match the requested type.");
    }
    
    /// <summary>
    /// Gets a cached repository of the specified type.
    /// </summary>
    /// <param name="repositoryType">
    /// The type of repository to get.
    /// </param>
    /// <returns>
    /// The cached repository object, or a not found result if it does not exist.
    /// </returns>
    public InfraResult<object> Get(Type repositoryType)
    {
        return _repositoryCache.TryGetValue(repositoryType, out var repositoryObject)
            ? InfraResult.Pass(repositoryObject)
            : InfraResult.NotFound<object>("the repository was not found in cache.");
    }
    
    /// <summary>
    /// Adds a repository instance to the cache.
    /// </summary>
    /// <param name="repository">
    /// The repository instance to add.
    /// </param>
    /// <typeparam name="TRepository">
    /// The type of the repository.
    /// </typeparam>
    public void Add<TRepository>(TRepository repository) 
        where TRepository : class
    {
        var type = repository.GetType();
        _repositoryCache[type] = repository;  
    }

    /// <summary>
    /// Removes a repository of the specified type from the cache.
    /// </summary>
    /// <typeparam name="TRepository">
    /// The type of repository to remove.
    /// </typeparam>
    /// <returns>
    /// The result of the removal operation.
    /// </returns>
    public InfraResult Remove<TRepository>()
    {
        return Remove(typeof(TRepository));
    }
    
    /// <summary>
    /// Removes a repository of the specified type from the cache.
    /// </summary>
    /// <param name="repositoryType">
    /// The type of repository to remove.
    /// </param>
    /// <returns>
    /// The result of the removal operation.
    /// </returns>
    public InfraResult Remove(Type repositoryType)
    {
        var isRemoved = _repositoryCache.Remove(repositoryType);
        return isRemoved
            ? InfraResult.Pass()
            : InfraResult.NotFound("the repository to remove was not found in cache.");
    }
    
    /// <summary>
    /// Clears all repositories from the cache.
    /// </summary>
    public void Clear()
    {
        _repositoryCache.Clear();
    }
}
using DDD.Core.Results;
using DDD.Core.UnitOfWork.Exceptions;

namespace DDD.Core.UnitOfWork;

public class RepositoryCache
{
    private readonly Dictionary<Type, object> _repositoryCache = new();
    
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
    
    public InfraResult<object> Get(Type repositoryType)
    {
        return _repositoryCache.TryGetValue(repositoryType, out var repositoryObject)
            ? InfraResult.Pass(repositoryObject)
            : InfraResult.NotFound<object>("the repository was not found in cache.");
    }
    
    public void Add<TRepository>(TRepository repository) 
        where TRepository : class
    {
        var type = repository.GetType();
        _repositoryCache[type] = repository;  
    }

    public bool Remove<TRepository>()
    {
        return Remove(typeof(TRepository));
    }
    
    public bool Remove(Type repositoryType)
    {
        return _repositoryCache.Remove(repositoryType);
    }
    
    public void Clear()
    {
        _repositoryCache.Clear();
    }
}
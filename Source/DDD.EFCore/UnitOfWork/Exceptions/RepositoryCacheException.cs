namespace DDD.Core.UnitOfWork.Exceptions;

/// <summary>
/// Exception thrown when there is an issue with the repository cache.
/// </summary>
public class RepositoryCacheException : Exception
{
    public  RepositoryCacheException()
    {
    }

    public RepositoryCacheException(string message) : base(message)
    {
        
    }
}
using System.Reflection;
using DDD.Core.Factories.Exceptions;
using DDD.Core.Results;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Factories;

/// <summary>
/// Factory class for creating repository instances.
/// </summary>
public static class RepositoryInstanceFactory
{
    /// <summary>
    /// Creates an instance of the specified repository type using the provided DbContext.
    /// The Repository constructor must only require the DbContext as a parameter or a DbSet of the DbContext.
    /// </summary>
    /// <param name="dbContext">
    /// The DbContext to be used for creating the repository instance.
    /// </param>
    /// <typeparam name="TDbContext">
    /// The type of the DbContext.
    /// </typeparam>
    /// <typeparam name="TRepository">
    /// The type of the repository to be created.
    /// </typeparam>
    /// <returns>
    /// The created repository instance.
    /// </returns>
    /// <exception cref="RepositoryFactoryException">
    /// Thrown if the repository instance could not be created.
    /// </exception>
    public static TRepository Create<TDbContext, TRepository>(TDbContext dbContext) 
        where TDbContext : DbContext
        where TRepository : class
    {
        var fromDbContextResult = CreateFromDbContext<TDbContext, TRepository>(dbContext);
        if (fromDbContextResult.IsSuccessful)
        {
            return fromDbContextResult.Output;
        }
        var fromDbSetResult = CreateFromDbSet<TDbContext, TRepository>(dbContext);
        if (fromDbSetResult.IsSuccessful)
        {
            return fromDbSetResult.Output;
        }
        throw new RepositoryFactoryException(
            $"Could not create repository instance {typeof(TRepository).Name}");
    }
    
    private static InfraResult<TRepository> CreateFromDbContext<TDbContext, TRepository>(TDbContext dbContext)
        where TDbContext : DbContext
        where TRepository : class
    {
        return CreateInstance<TRepository>(dbContext);
    }
    
    private static InfraResult<TRepository> CreateFromDbSet<TDbContext, TRepository>(TDbContext dbContext)
        where TDbContext : DbContext
        where TRepository : class
    {
        foreach (var dbSetProperty in GetAllDbSets<TDbContext>())
        {
            var result = CreateFromDbSetProperty<TDbContext, TRepository>(dbContext, dbSetProperty);
            if (result.IsSuccessful)
            {
                return result;
            }
        }
        return InfraResult.Fail<TRepository>("could not create repository from any DbSet.");
    }
    
    private static InfraResult<TRepository> CreateFromDbSetProperty<TDbContext, TRepository>(TDbContext dbContext, PropertyInfo dbSetProperty)
        where TDbContext : DbContext
        where TRepository : class
    {
        var dbSetInstance = dbSetProperty.GetValue(dbContext);
        return dbSetInstance is not null 
            ? CreateInstance<TRepository>(dbSetInstance)
            : InfraResult.Fail<TRepository>("DbSet instance is null.");
    }
    
    
    private static List<PropertyInfo> GetAllDbSets<TDbContext>()    
        where TDbContext : DbContext
    {
        return typeof(TDbContext).GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .ToList();
    }


    private static InfraResult<TRepository> CreateInstance<TRepository>(params object[] args)   
        where TRepository : class
    {
        try
        {
            var instance = Activator.CreateInstance(typeof(TRepository), args);
            return instance is not TRepository repo 
                ? InfraResult.Fail<TRepository>("could not create repository instance.") 
                : InfraResult.Pass(repo);
        }
        catch (Exception e)
        {
            return InfraResult.Fail<TRepository>($"of the following error: {e.Message}");
        }
    }
    
}
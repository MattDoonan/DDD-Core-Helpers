namespace DDD.Core.UnitOfWork.Interfaces;

/// <summary>
/// Marker interface for single aggregate root repositories.
/// </summary>
public interface ISingleRepository;

/// <summary>
/// A repository interface for managing write operations on a single type of aggregate root.
/// Provides methods for adding and updating aggregate roots.
/// </summary>
/// <typeparam name="TAggregate"></typeparam>
public interface ISingleRepository<in TAggregate> : ISingleRepository
    where TAggregate : class
{
    void Add(TAggregate aggregateRoot);
    void AddMany(IEnumerable<TAggregate> aggregateRoots);
    void Update(TAggregate aggregateRoot);
}
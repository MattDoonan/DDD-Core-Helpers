namespace DDD.Core.UnitOfWork.Interfaces;

public interface ISingleRepository;

public interface ISingleRepository<in TAggregate> : ISingleRepository
    where TAggregate : class
{
    void Add(TAggregate aggregateRoot);
    void AddMany(IEnumerable<TAggregate> aggregateRoots);
    void Update(TAggregate aggregateRoot);
}
using DDD.Core.Entities;
using DDD.Core.Entities.Interfaces;
using DDD.Core.Results;
using DDD.Core.UnitOfWork;
using DDD.Core.UnitOfWork.Interfaces;

namespace DDD.Core.Repositories;

public class PersistenceRepository<T, TDbContext>
    where T : Entity, IAggregateRoot
    where TDbContext : DbContext
{
    private readonly WriteUnitOfWork<TDbContext> _unitOfWork;

    public PersistenceRepository(TDbContext context)
    {
        _unitOfWork = new WriteUnitOfWork<TDbContext>(context);
    }

    public async Task<RepoResult> AddAsync(T aggregateRoot, CancellationToken token = default)
    {
        _unitOfWork.Add(aggregateRoot);
        return await SaveAsync(token);
    }
    
    public async Task<RepoResult> AddManyAsync(IEnumerable<T> aggregateRoots, CancellationToken token = default)
    {
        _unitOfWork.AddMany(aggregateRoots);
        return await SaveAsync(token);
    }

    public async Task<RepoResult> UpdateAsync(T aggregateRoot, CancellationToken token = default)
    {
        _unitOfWork.Update(aggregateRoot);
        return await SaveAsync(token);
    }
    
    public async Task<RepoResult> UpdateManyAsync(IEnumerable<T> aggregateRoots, CancellationToken token = default)
    {
        _unitOfWork.UpdateMany(aggregateRoots);
        return await SaveAsync(token);
    }
    
    public Task<RepoResult> SaveAsync(CancellationToken token = default)
    {
        return _unitOfWork.SaveChangesAsync(token);
    }
}
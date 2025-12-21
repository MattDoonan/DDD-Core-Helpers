using DDD.Core.Repositories;
using DDD.Core.UnitOfWork;
using EFCore.Tests.Helpers;
using Xunit;

namespace EFCore.Tests.UnitOfWork;

public class UnitOfWorkContextTests
{
    private sealed class TestUnitOfWorkContext : UnitOfWorkContext<TestDbContext>
    {
        public SingleRepository<TestAggregate> TestRepository => LazyGet<SingleRepository<TestAggregate>>();

        public TestUnitOfWorkContext(TestDbContext dbContext) : base(dbContext)
        {
        }

    }
    
    [Fact]
    public void GetSingleUnitOfWork_ShouldCreateInstance_WhenCalled()
    {
        var options = ContextOptionsFactory.Create<TestDbContext>();
        using var dbContext = new TestDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repository = uowContext.TestRepository;
        Assert.NotNull(repository);
        Assert.IsType<SingleRepository<TestAggregate>>(repository);
    }

    [Fact]
    public void GetSingleUnitOfWork_ShouldReturnSameInstance_OnMultipleCalls()
    {
        var options = ContextOptionsFactory.Create<TestDbContext>();
        using var dbContext = new TestDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repo1 = uowContext.TestRepository;
        var repo2 = uowContext.TestRepository;
        Assert.Same(repo1, repo2);
    }
    
    [Fact]
    public async Task SaveChanges_ShouldCallDbContextSaveChanges()
    {
        var options = ContextOptionsFactory.Create<TestDbContext>();
        await using var dbContext = new TestDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        uowContext.TestRepository.Add(new TestAggregate());
        var changes = await uowContext.SaveChangesAsync();
        Assert.True(changes.IsSuccessful);
        Assert.Single(dbContext.TestAggregates);
    }

}
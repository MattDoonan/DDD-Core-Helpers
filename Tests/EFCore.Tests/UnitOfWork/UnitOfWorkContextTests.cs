using DDD.Core.Repositories;
using DDD.Core.UnitOfWork;
using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Xunit;

namespace EFCore.Tests.UnitOfWork;

public class UnitOfWorkContextTests
{
    private sealed class TestUnitOfWorkContext : UnitOfWorkContext<SocialsDbContext>
    {
        public SingleRepository<Account> Accounts => LazyGet<SingleRepository<Account>>();
        public SingleRepository<Post> Posts => LazyGet<SingleRepository<Post>>();
        public SingleRepository<Comment> Comments => LazyGet<SingleRepository<Comment>>();

        public TestUnitOfWorkContext(SocialsDbContext dbContext) : base(dbContext)
        {
        }

    }
    
    [Fact]
    public void GetSingleUnitOfWork_ShouldCreateInstance_WhenCalled()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repository = uowContext.Accounts;
        Assert.NotNull(repository);
        Assert.IsType<SingleRepository<Account>>(repository);
    }

    [Fact]
    public void GetSingleUnitOfWork_ShouldReturnSameInstance_OnMultipleCalls()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repo1 = uowContext.Accounts;
        var repo2 = uowContext.Accounts;
        Assert.Same(repo1, repo2);
    }
    
    [Fact]
    public async Task SaveChanges_ShouldCallDbContextSaveChanges()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        uowContext.Accounts.Add(new Account());
        var changes = await uowContext.SaveChangesAsync();
        Assert.True(changes.IsSuccessful);
        Assert.Single(dbContext.Accounts);
    }

    [Fact]
    public void GetSingleUnitOfWork_ShouldCreateNewRepoForDifferentTypes()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var postRepo = uowContext.Posts;
        Assert.NotSame(accountRepo, postRepo);
    }
    
    [Fact]
    public void GetSingleUnitOfWork_ShouldCacheNewRepoForDifferentTypes()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var postRepo = uowContext.Posts;
        var accountRepo2 = uowContext.Accounts;
        var postRepo2 = uowContext.Posts;
        Assert.Same(accountRepo, accountRepo2);
        Assert.Same(postRepo, postRepo2);
    }

    [Fact]
    public async Task SaveChanges_ShouldReturnFailureResult_WhenExceptionOccurs()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        await dbContext.DisposeAsync();
        var result = await uowContext.SaveChangesAsync();
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public void RemoveFromCache_ShouldRemoveCachedRepository_WhenCalled()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var isSuccessful = uowContext.RemoveFromCache<SingleRepository<Account>>();
        Assert.True(isSuccessful);
        var accountRepo2 = uowContext.Accounts;
        Assert.NotSame(accountRepo, accountRepo2);
    }

    [Fact]
    public void ClearCache_ShouldRemoveAllCachedRepositories_WhenCalled()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var dbContext = new SocialsDbContext(options);
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var postRepo = uowContext.Posts;
        var commentRepo = uowContext.Comments;
        uowContext.ClearCache();
        var accountRepo2 = uowContext.Accounts;
        var postRepo2 = uowContext.Posts;
        var commentRepo2 = uowContext.Comments;
        Assert.NotSame(accountRepo, accountRepo2);
        Assert.NotSame(postRepo, postRepo2);
        Assert.NotSame(commentRepo, commentRepo2);
    }
}
using DDD.Core.Repositories;
using DDD.Core.UnitOfWork;
using DDD.Core.UnitOfWork.Interfaces;
using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Xunit;

namespace EFCore.Tests.UnitOfWork;

public class UnitOfWorkContextTests
{
    private sealed class LikesRepository : ISingleRepository
    {
        public int Get()
        { 
            return 42;
        }
    }
    
    private sealed class TestUnitOfWorkContext : UnitOfWorkContext<SocialsDbContext>
    {
        public SingleRepository<Account> Accounts => LazyGet<SingleRepository<Account>>();
        public SingleRepository<Post> Posts => LazyGet<SingleRepository<Post>>();
        public SingleRepository<Comment> Comments => LazyGet<SingleRepository<Comment>>();
        public LikesRepository Likes => LazyGet(() => new LikesRepository());

        public TestUnitOfWorkContext(SocialsDbContext dbContext) : base(dbContext)
        {
        }

    }
    
    [Fact]
    public void GetSingleUnitOfWork_ShouldCreateInstance_WhenCalled()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repository = uowContext.Accounts;
        Assert.NotNull(repository);
        Assert.IsType<SingleRepository<Account>>(repository);
    }
    
    [Fact]
    public void GetSingleUnitOfWork_UsingLazyGet_WithPredicate_ShouldCreateInstance_WhenCalled()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repository = uowContext.Likes;
        Assert.NotNull(repository);
        Assert.IsType<LikesRepository>(repository);
    }

    [Fact]
    public void GetSingleUnitOfWork_ShouldReturnSameInstance_OnMultipleCalls()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repo1 = uowContext.Accounts;
        var repo2 = uowContext.Accounts;
        Assert.Same(repo1, repo2);
    }
    
    [Fact]
    public void GetSingleUnitOfWork_UsingLazyGet_WithPredicate_ShouldReturnSameInstance_OnMultipleCalls()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var repository = uowContext.Likes;
        var repository2 = uowContext.Likes;
        Assert.Same(repository, repository2);
    }
    
    [Fact]
    public async Task SaveChanges_ShouldCallDbContextSaveChanges()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        uowContext.Accounts.Add(new Account());
        var changes = await uowContext.SaveChangesAsync();
        Assert.True(changes.IsSuccessful);
        Assert.Single(dbContext.Accounts);
    }

    [Fact]
    public void GetSingleUnitOfWork_ShouldCreateNewRepoForDifferentTypes()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var postRepo = uowContext.Posts;
        Assert.NotSame(accountRepo, postRepo);
    }
    
    [Fact]
    public void GetSingleUnitOfWork_ShouldCacheNewRepoForDifferentTypes()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
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
        var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        await dbContext.DisposeAsync();
        var result = await uowContext.SaveChangesAsync();
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public void RemoveFromCache_ShouldRemoveCachedRepository_WhenCalled()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var uowContext = new TestUnitOfWorkContext(dbContext);
        var accountRepo = uowContext.Accounts;
        var removalResult = uowContext.RemoveFromCache<SingleRepository<Account>>();
        Assert.True(removalResult.IsSuccessful);
        var accountRepo2 = uowContext.Accounts;
        Assert.NotSame(accountRepo, accountRepo2);
    }

    [Fact]
    public void ClearCache_ShouldRemoveAllCachedRepositories_WhenCalled()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
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
using DDD.Core.Repositories;
using DDD.Core.UnitOfWork;
using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Xunit;

namespace EFCore.Tests.UnitOfWork;

public class RepositoryCacheTests
{
    [Fact]
    public void Add_ThenGetRepositoryByGenericArgument_ShouldReturnSameInstance_ForSameType()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(repository);
        
        var getResult = cache.Get<SingleRepository<Account>>();
        Assert.True(getResult.IsSuccessful);
        Assert.Same(repository, getResult.Output);
    }
    
    [Fact]
    public void Add_ThenGetRepositoryByType_ShouldReturnSameInstance_ForSameType()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(repository);
        
        var getResult = cache.Get(repository.GetType());
        Assert.True(getResult.IsSuccessful);
        Assert.IsType<SingleRepository<Account>>(getResult.Output);
        Assert.Same(repository, getResult.Output);
    }

    [Fact]
    public void GetRepositoryByGenericArgument_ShouldReturnDifferentInstances_ForDifferentTypes()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var accountRepository = new SingleRepository<Account>(dbContext.Accounts);
        var postRepository = new SingleRepository<Post>(dbContext.Posts);
        var cache = new RepositoryCache();
        cache.Add(accountRepository);
        cache.Add(postRepository);
        
        var getAccountResult = cache.Get<SingleRepository<Account>>();
        var getPostResult = cache.Get<SingleRepository<Post>>();
        
        Assert.True(getAccountResult.IsSuccessful);
        Assert.True(getPostResult.IsSuccessful);
        Assert.Same(accountRepository, getAccountResult.Output);
        Assert.Same(postRepository, getPostResult.Output);
        Assert.NotSame(getAccountResult.Output, getPostResult.Output);
    }
    
    [Fact]
    public void GetRepositoryByGenericArgument_WithSameDbSet_ShouldReturnDifferentInstances_ForDifferentTypes()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var getRepository = new GetRepository<AccountId, Account>(dbContext.Accounts);
        var singleRepository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(getRepository);
        cache.Add(singleRepository);
        
        var getRepositoryResult = cache.Get<GetRepository<AccountId, Account>>();
        var getSingleRepositoryResult = cache.Get<SingleRepository<Account>>();
        
        Assert.True(getRepositoryResult.IsSuccessful);
        Assert.True(getSingleRepositoryResult.IsSuccessful);
        Assert.Same(getRepository, getRepositoryResult.Output);
        Assert.Same(singleRepository, getSingleRepositoryResult.Output);
        Assert.NotSame(getRepositoryResult.Output, getSingleRepositoryResult.Output);
    }

    [Fact]
    public void GetRepositoryByType_ShouldReturnDifferentInstances_ForDifferentTypes()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var accountRepository = new SingleRepository<Account>(dbContext.Accounts);
        var postRepository = new SingleRepository<Post>(dbContext.Posts);
        var cache = new RepositoryCache();
        cache.Add(accountRepository);
        cache.Add(postRepository);
        
        var getAccountResult = cache.Get(accountRepository.GetType());
        var getPostResult = cache.Get(postRepository.GetType());
        
        Assert.True(getAccountResult.IsSuccessful);
        Assert.True(getPostResult.IsSuccessful);
        Assert.Same(accountRepository, getAccountResult.Output);
        Assert.Same(postRepository, getPostResult.Output);
        Assert.NotSame(getAccountResult.Output, getPostResult.Output);
    }

    [Fact]
    public void GetRepositoryByGenericArgument_WhenRepositoryNotInCache_ShouldReturnFailure()
    {
        var cache = new RepositoryCache();
        var getResult = cache.Get<SingleRepository<Account>>();
        Assert.True(getResult.IsFailure);
    }

    [Fact]
    public void GetRepositoryByType_WhenRepositoryNotInCache_ShouldReturnFailure()
    {
        var cache = new RepositoryCache();
        var getResult = cache.Get(typeof(SingleRepository<Account>));
        Assert.True(getResult.IsFailure);
    }

    [Fact]
    public void Add_SameRepositoryTypeMultipleTimes_ShouldOverwritePreviousInstance()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var firstRepository = new SingleRepository<Account>(dbContext.Accounts);
        var secondRepository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(firstRepository);
        cache.Add(secondRepository);
        
        var getResult = cache.Get<SingleRepository<Account>>();
        Assert.True(getResult.IsSuccessful);
        Assert.Same(secondRepository, getResult.Output);
        Assert.NotSame(firstRepository, getResult.Output);
    }
    
    [Fact]
    public void RemoveRepositoryByGenericArgument_ShouldMakeRepositoryUnavailableInCache()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(repository);
        
        var removalResult = cache.Remove<SingleRepository<Account>>();
        Assert.True(removalResult.IsSuccessful);
        
        var getResult = cache.Get<SingleRepository<Account>>();
        Assert.True(getResult.IsFailure);
    }

    [Fact]
    public void RemoveRepositoryByType_ShouldMakeRepositoryUnavailableInCache()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = new SingleRepository<Account>(dbContext.Accounts);
        var cache = new RepositoryCache();
        cache.Add(repository);
        
        var removalResult = cache.Remove(repository.GetType());
        Assert.True(removalResult.IsSuccessful);
        
        var getResult = cache.Get<SingleRepository<Account>>();
        Assert.True(getResult.IsFailure);
    }
    
    [Fact]
    public void RemoveRepositoryByGenericArgument_WhenRepositoryNotInCache_ShouldReturnFailure()
    {
        var cache = new RepositoryCache();
        var removalResult = cache.Remove<SingleRepository<Account>>();
        Assert.True(removalResult.IsFailure);
        Assert.True(removalResult.IsNotFound);
    }

    [Fact]
    public void RemoveRepositoryByType_WhenRepositoryNotInCache_ShouldReturnFailure()
    {
        var cache = new RepositoryCache();
        var removalResult = cache.Remove(typeof(SingleRepository<Account>));
        Assert.True(removalResult.IsFailure);
        Assert.True(removalResult.IsNotFound);
    }

    [Fact]
    public void Clear_ShouldMakeAllRepositoriesUnavailableInCache()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var accountRepository = new SingleRepository<Account>(dbContext.Accounts);
        var postRepository = new SingleRepository<Post>(dbContext.Posts);
        var cache = new RepositoryCache();
        cache.Add(accountRepository);
        cache.Add(postRepository);
        
        cache.Clear();
        
        var getAccountResult = cache.Get<SingleRepository<Account>>();
        var getPostResult = cache.Get<SingleRepository<Post>>();
        
        Assert.True(getAccountResult.IsFailure);
        Assert.True(getPostResult.IsFailure);
    }
}


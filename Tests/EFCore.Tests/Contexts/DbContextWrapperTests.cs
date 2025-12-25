using DDD.Core.Contexts;
using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Xunit;

namespace EFCore.Tests.Contexts;

public class DbContextWrapperTests
{
    [Fact]
    public void TestDbContextWrapper_Creation_Succeeds()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        Assert.NotNull(wrapper);
        Assert.IsType<DbContextWrapper<SocialsDbContext>>(wrapper);
    }

    [Fact]
    public async Task SaveChangesAsync_NoChanges_ReturnsSuccess()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WithChanges_ReturnsSuccess()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        
        var account = new Account();
        dbContext.Accounts.Add(account);
        
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WithInvalidChanges_ReturnsFailure()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        var account = new Account(AccountId.Create().Output);
        dbContext.Accounts.Add(account);
        var result1 = await wrapper.SaveChangesAsync();
        Assert.True(result1.IsSuccessful);
        var accountDuplicate = dbContext.Accounts.First();
        dbContext.Accounts.Add(accountDuplicate); 
        var result2 = await wrapper.SaveChangesAsync();
        Assert.True(result2.IsFailure);
    }
    
    [Fact]
    public async Task SaveChangesAsync_CancellationRequested_ReturnsFailure()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var result = await wrapper.SaveChangesAsync(cts.Token);
        Assert.True(result.IsFailure);
        Assert.True(result.OperationIsCancelled);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WhenDbContextIsDisposed_Should_ReturnFailure()
    {
        var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        await dbContext.DisposeAsync();
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsFailure);
    }

    [Fact]
    public void SaveChanges_NoChanges_ReturnsSuccess()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        var result = wrapper.SaveChanges();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void SaveChanges_WithChanges_ReturnsSuccess()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        
        var account = new Account();
        dbContext.Accounts.Add(account);
        
        var result = wrapper.SaveChanges();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void SaveChanges_WithInvalidChanges_ReturnsFailure()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        var account = new Account(AccountId.Create().Output);
        dbContext.Accounts.Add(account);
        var result1 = wrapper.SaveChanges();
        Assert.True(result1.IsSuccessful);
        var accountDuplicate = dbContext.Accounts.First();
        dbContext.Accounts.Add(accountDuplicate); 
        var result2 = wrapper.SaveChanges();
        Assert.True(result2.IsFailure);
    }

    [Fact]
    public void SaveChanges_WhenDbContextIsDisposed_Should_ReturnFailure()
    {
        var dbContext = ContextFactory.Create<SocialsDbContext>();
        var wrapper = new DbContextWrapper<SocialsDbContext>(dbContext);
        dbContext.Dispose();
        var result = wrapper.SaveChanges();
        Assert.True(result.IsFailure);
    }
}
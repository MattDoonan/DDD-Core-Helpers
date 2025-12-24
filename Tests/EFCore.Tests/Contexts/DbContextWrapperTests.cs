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
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        Assert.NotNull(wrapper);
        Assert.IsType<DbContextWrapper<SocialsDbContext>>(wrapper);
    }

    [Fact]
    public async Task SaveChangesAsync_NoChanges_ReturnsSuccess()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WithChanges_ReturnsSuccess()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        
        var account = new Account();
        context.Accounts.Add(account);
        
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WithInvalidChanges_ReturnsFailure()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        var account = new Account(AccountId.Create().Output);
        context.Accounts.Add(account);
        var result1 = await wrapper.SaveChangesAsync();
        Assert.True(result1.IsSuccessful);
        var accountDuplicate = context.Accounts.First();
        context.Accounts.Add(accountDuplicate); 
        var result2 = await wrapper.SaveChangesAsync();
        Assert.True(result2.IsFailure);
    }
    
    [Fact]
    public async Task SaveChangesAsync_CancellationRequested_ReturnsFailure()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var result = await wrapper.SaveChangesAsync(cts.Token);
        Assert.True(result.IsFailure);
        Assert.True(result.OperationIsCancelled);
    }
    
    [Fact]
    public async Task SaveChangesAsync_WhenDbContextIsDisposed_Should_ReturnFailure()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        await context.DisposeAsync();
        var result = await wrapper.SaveChangesAsync();
        Assert.True(result.IsFailure);
    }

    [Fact]
    public void SaveChanges_NoChanges_ReturnsSuccess()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        var result = wrapper.SaveChanges();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void SaveChanges_WithChanges_ReturnsSuccess()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        
        var account = new Account();
        context.Accounts.Add(account);
        
        var result = wrapper.SaveChanges();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void SaveChanges_WithInvalidChanges_ReturnsFailure()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        var account = new Account(AccountId.Create().Output);
        context.Accounts.Add(account);
        var result1 = wrapper.SaveChanges();
        Assert.True(result1.IsSuccessful);
        var accountDuplicate = context.Accounts.First();
        context.Accounts.Add(accountDuplicate); 
        var result2 = wrapper.SaveChanges();
        Assert.True(result2.IsFailure);
    }

    [Fact]
    public void SaveChanges_WhenDbContextIsDisposed_Should_ReturnFailure()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        var context = new SocialsDbContext(options);
        var wrapper = new DbContextWrapper<SocialsDbContext>(context);
        context.Dispose();
        var result = wrapper.SaveChanges();
        Assert.True(result.IsFailure);
    }
}
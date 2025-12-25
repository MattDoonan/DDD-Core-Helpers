using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFCore.Tests.Contexts;

public class DomainDbContextTests
{
    [Fact]
    public void TestDbContext_Creation_Succeeds()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        Assert.NotNull(dbContext);
        Assert.IsType<SocialsDbContext>(dbContext);
    }
    
    [Fact]
    public async Task AddEntity_ThatHasValueObject_WithSingleValueObjectFactory_Should_UseConverter()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var expectedPostId = PostId.Create().Output;
        var post = new Post(expectedPostId);
        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync();
        var retrievedPost = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == expectedPostId);
        Assert.NotNull(retrievedPost);
        Assert.Equal(expectedPostId, retrievedPost.Id);
    }
    
    [Fact]
    public async Task AddEntity_ThatHasValueObject_WithConvertibleFactory_Should_UseConverter()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var expectedCommentId = CommentId.From(1);
        var comment = new Comment(expectedCommentId);
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        var retrievedComment = await dbContext.Comments.FirstOrDefaultAsync(p => p.Id == expectedCommentId);
        Assert.NotNull(retrievedComment);
        Assert.Equal(expectedCommentId, retrievedComment.Id);
    }
    
    [Fact]
    public async Task AddEntity_ThatGeneratesAGuidOnAdd_Should_GenerateGuidOnAdd()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var account = new Account();
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        Assert.Single(dbContext.Accounts);
        var retrievedAccount = await dbContext.Accounts.FirstOrDefaultAsync();
        Assert.NotNull(retrievedAccount);
        Assert.NotNull(retrievedAccount.Id);
        Assert.NotEqual(Guid.Empty, retrievedAccount.Id.Value);
    }
    
    
    [Fact]
    public async Task AddEntity_ThatGeneratesANumberOnAdd_Should_GenerateNumberOnAdd()
    {
        await using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var comment = new Comment();
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        Assert.Single(dbContext.Comments);
        var retrievedComment = await dbContext.Comments.FirstOrDefaultAsync();
        Assert.NotNull(retrievedComment);
        Assert.NotNull(retrievedComment.Id);
        Assert.NotEqual((uint)0, retrievedComment.Id.Value);
    }
    
    
    
}
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
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        using var context = new SocialsDbContext(options);
        Assert.NotNull(context);
        Assert.IsType<SocialsDbContext>(context);
    }
    
    [Fact]
    public async Task AddEntity_ThatHasValueObject_WithSingleValueObjectFactory_Should_UseConverter()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var expectedPostId = PostId.Create().Output;
        var post = new Post(expectedPostId);
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        var retrievedPost = await context.Posts.FirstOrDefaultAsync(p => p.Id == expectedPostId);
        Assert.NotNull(retrievedPost);
        Assert.Equal(expectedPostId, retrievedPost.Id);
    }
    
    [Fact]
    public async Task AddEntity_ThatHasValueObject_WithConvertibleFactory_Should_UseConverter()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var expectedCommentId = CommentId.From(1);
        var comment = new Comment(expectedCommentId);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        var retrievedComment = await context.Comments.FirstOrDefaultAsync(p => p.Id == expectedCommentId);
        Assert.NotNull(retrievedComment);
        Assert.Equal(expectedCommentId, retrievedComment.Id);
    }
    
    [Fact]
    public async Task AddEntity_ThatGeneratesAGuidOnAdd_Should_GenerateGuidOnAdd()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var account = new Account();
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
        Assert.Single(context.Accounts);
        var retrievedAccount = await context.Accounts.FirstOrDefaultAsync();
        Assert.NotNull(retrievedAccount);
        Assert.NotNull(retrievedAccount.Id);
        Assert.NotEqual(Guid.Empty, retrievedAccount.Id.Value);
    }
    
    
    [Fact]
    public async Task AddEntity_ThatGeneratesANumberOnAdd_Should_GenerateNumberOnAdd()
    {
        var options = ContextOptionsFactory.Create<SocialsDbContext>();
        await using var context = new SocialsDbContext(options);
        var comment = new Comment();
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        Assert.Single(context.Comments);
        var retrievedComment = await context.Comments.FirstOrDefaultAsync();
        Assert.NotNull(retrievedComment);
        Assert.NotNull(retrievedComment.Id);
        Assert.NotEqual((uint)0, retrievedComment.Id.Value);
    }
    
    
    
}
using DDD.Core.Contexts;
using DDD.Core.Extensions;
using EFCore.Tests.Helpers.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Tests.Helpers.Contexts;

public class SocialsDbContext : DomainDbContext
{
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public SocialsDbContext(DbContextOptions<SocialsDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ConfigureGeneratedId<AccountId, Account>();
        });
        modelBuilder.Entity<Post>(entity =>
        {
            entity.ConfigureGeneratedId<PostId, Post>();
        });
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ConfigureGeneratedId<CommentId, Comment>();
        });
    }
}
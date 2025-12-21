using DDD.Core.Contexts;
using DDD.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Tests.Helpers;

public class TestDbContext : DomainDbContext
{
    
    public DbSet<TestAggregate> TestAggregates { get; set; }
    
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TestAggregate>(entity =>
        {
            entity.ConfigureGeneratedId<TestId, TestAggregate>();
        });
    }
}
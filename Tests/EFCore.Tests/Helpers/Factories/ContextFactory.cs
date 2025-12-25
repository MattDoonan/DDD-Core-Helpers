using Microsoft.EntityFrameworkCore;

namespace EFCore.Tests.Helpers.Factories;

public static class ContextFactory
{
    public static TContext Create<TContext>()
        where TContext : DbContext
    {
        var options = CreateOptions<TContext>();
        return (TContext)Activator.CreateInstance(typeof(TContext), options)!;
    }
    
    private static DbContextOptions<TContext> CreateOptions<TContext>()
        where TContext : DbContext
    {
        return new DbContextOptionsBuilder<TContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}
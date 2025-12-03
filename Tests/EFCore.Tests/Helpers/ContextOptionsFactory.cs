using Microsoft.EntityFrameworkCore;

namespace EFCore.Tests.Helpers;

public static class ContextOptionsFactory
{
    public static DbContextOptions<TContext> Create<TContext>()
        where TContext : DbContext
    {
        return new DbContextOptionsBuilder<TContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}
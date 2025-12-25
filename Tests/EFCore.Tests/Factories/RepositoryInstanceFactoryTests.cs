using DDD.Core.Factories;
using DDD.Core.Factories.Exceptions;
using EFCore.Tests.Helpers.Contexts;
using EFCore.Tests.Helpers.Factories;
using EFCore.Tests.Helpers.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFCore.Tests.Factories;

public class RepositoryInstanceFactoryTests
{
    private sealed class RepositoryWithDbContextConstructor
    {
        public RepositoryWithDbContextConstructor(SocialsDbContext _)
        {
        }
    }
    
    private sealed class RepositoryWithDbSetConstructor
    {
        public RepositoryWithDbSetConstructor(DbSet<Account> _)
        {
        }
    }
    
    private sealed class NoValidConstructorRepository
    {
        
    }

    [Fact]
    public void CreateInstance_ShouldCreateInstance_WhenConstructorWithDbContextExists()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = RepositoryInstanceFactory.Create<SocialsDbContext, RepositoryWithDbContextConstructor>(dbContext);
        Assert.NotNull(repository);
        Assert.IsType<RepositoryWithDbContextConstructor>(repository);
    }

    [Fact]
    public void CreateInstance_ShouldCreateInstance_WhenConstructorWithDbSetExists()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        var repository = RepositoryInstanceFactory.Create<SocialsDbContext, RepositoryWithDbSetConstructor>(dbContext);
        Assert.NotNull(repository);
        Assert.IsType<RepositoryWithDbSetConstructor>(repository);
    }

    [Fact]
    public void CreateInstance_ShouldThrowException_WhenNoValidConstructorExists()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        Assert.Throws<RepositoryFactoryException>(() =>
        {
            RepositoryInstanceFactory.Create<SocialsDbContext, NoValidConstructorRepository>(dbContext);
        });
    }

    [Fact]
    public void CreateInstance_ShouldThrowException_WhenRepositoryTypeIsInvalid()
    {
        using var dbContext = ContextFactory.Create<SocialsDbContext>();
        Assert.Throws<RepositoryFactoryException>(() =>
        {
            RepositoryInstanceFactory.Create<SocialsDbContext, string>(dbContext);
        });
    }
}
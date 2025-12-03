using DDD.Core.Extensions;
using EFCore.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFCore.Tests.Extensions;

public class RepositoryProviderExtensions
{
    [Fact]
    public void Test()
    {
        var context = new DbContext(ContextOptionsFactory.Create<DbContext>());
        var getRepository = context.CreateGetRepository<TestId, TestAggregate>();
        Assert.NotNull(getRepository);
        var getRepository2 = context.CreateGetRepository<TestId, TestAggregate>();
    }
}
using DDD.Core.Entities;

namespace EFCore.Tests.Helpers;

public class TestAggregate : AggregateRoot<TestId>
{
    public TestAggregate()
    {
    }
    
    public TestAggregate(TestId id) : base(id)
    {
    }
}
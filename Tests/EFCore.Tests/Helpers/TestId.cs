using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Types;

namespace EFCore.Tests.Helpers;

public record TestId : GuidAggregateRootId<TestId>, IGuidFactory<TestId>
{
    public TestId(Guid Value) : base(Value)
    {
    }

    public static ValueObjectResult<TestId> Create(Guid value)
    {
        return new TestId(value);
    }
}
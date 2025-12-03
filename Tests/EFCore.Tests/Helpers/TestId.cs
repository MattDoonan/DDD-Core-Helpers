using DDD.Core.Entities;
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Types;

namespace EFCore.Tests.Helpers;

public record TestId : GuidAggregateRootId<TestId>, IGuidFactory<TestId>
{
    private TestId(Guid Value) : base(Value)
    {
    }

    public static ValueObjectResult<TestId> Create(Guid value)
    {
        if (value.Equals(Guid.Empty))
        {
            return ValueObjectResult.InvalidInput<TestId>();
        }
        return new TestId(value);
    }
}
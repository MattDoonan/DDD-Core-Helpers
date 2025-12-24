using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Types;

namespace EFCore.Tests.Helpers.Models;

public record PostId : GuidAggregateRootId<PostId>, IGuidFactory<PostId>
{
    private PostId(Guid Value) : base(Value)
    {
    }

    public static ValueObjectResult<PostId> Create(Guid value)
    {
        return new PostId(value);
    }
}
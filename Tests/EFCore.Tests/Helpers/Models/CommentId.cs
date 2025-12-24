using DDD.Core.Interfaces.Factories;
using DDD.Core.ValueObjects.Identifiers;

namespace EFCore.Tests.Helpers.Models;

public record CommentId : AggregateRootId<uint>, IConvertibleFactory<uint, CommentId>
{
    public CommentId(uint Value) : base(Value)
    {
    }

    public static CommentId From(uint value)
    {
        return new CommentId(value);
    }
}
using DDD.Core.Results;
using DDD.Core.ValueObjects.Factories;
using DDD.Core.ValueObjects.Identifiers.Types;

namespace EFCore.Tests.Helpers.Models;

public record AccountId : GuidAggregateRootId<AccountId>, IGuidFactory<AccountId>
{
    private AccountId(Guid Value) : base(Value)
    {
    }

    public static ValueObjectResult<AccountId> Create(Guid value)
    {
        return new AccountId(value);
    }
}
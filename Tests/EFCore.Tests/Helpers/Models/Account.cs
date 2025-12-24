using DDD.Core.Entities;

namespace EFCore.Tests.Helpers.Models;

public class Account : AggregateRoot<AccountId>
{
    public Account()
    {
    }
    
    public Account(AccountId id) : base(id)
    {
    }
}
using DDD.Core.Entities;

namespace EFCore.Tests.Helpers.Models;

public class Post : AggregateRoot<PostId>
{
    public Post(PostId id) : base(id)
    {
    }
    
    public  Post()
    {
    }
    
}
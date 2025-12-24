using DDD.Core.Entities;

namespace EFCore.Tests.Helpers.Models;

public class Comment : AggregateRoot<CommentId>
{
    public Comment(CommentId id) : base(id)
    {
    }

    public Comment()
    {
        
    }
}
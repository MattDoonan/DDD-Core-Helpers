using DDD.Core.Entities;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Core.Extensions;

public static class EntityExtension
{
    public static void ConfigureGeneratedId<TId, TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TId : IIdentifier
        where TEntity : Entity<TId>
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
    
    public static void ConfigureId<TId, TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TId : IIdentifier
        where TEntity : Entity<TId>
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
    }
}
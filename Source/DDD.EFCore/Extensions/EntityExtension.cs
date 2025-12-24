using DDD.Core.Entities;
using DDD.Core.ValueObjects.Identifiers.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Core.Extensions;

public static class EntityExtension
{
    /// <summary>
    /// Configures the entity to use a generated identifier on add.
    /// </summary>
    /// <param name="builder">
    /// The EntityTypeBuilder for the entity to configure
    /// </param>
    /// <typeparam name="TId">
    /// The type of the identifier
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    public static void ConfigureGeneratedId<TId, TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TId : IIdentifier
        where TEntity : Entity<TId>
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
    
    /// <summary>
    /// Configures the entity to use a non-generated identifier.
    /// </summary>
    /// <param name="builder">
    /// The EntityTypeBuilder for the entity to configure
    /// </param>
    /// <typeparam name="TId">
    /// The type of the identifier
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    public static void ConfigureId<TId, TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TId : IIdentifier
        where TEntity : Entity<TId>
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
    }
}
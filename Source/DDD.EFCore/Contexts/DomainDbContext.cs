using DDD.Core.Converters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Contexts;

/// <summary>
/// A <see cref="DbContext"/> that is configured to handle single value objects using a custom value converter selector.
/// </summary>
public class DomainDbContext : DbContext
{
    public DomainDbContext(DbContextOptions options) 
        : base(options)
    {
    }
    
    /// <summary>
    /// Configures the database context to use the custom value converter selector for single value objects.
    /// </summary>
    /// <param name="optionsBuilder">
    /// The builder used to create or modify options for this context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IValueConverterSelector, SingleValueObjectConverterSelector>();
    }
}
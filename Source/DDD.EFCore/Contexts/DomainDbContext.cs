using DDD.Core.Converters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Contexts;

public class DomainDbContext : DbContext
{
    public DomainDbContext(DbContextOptions options) 
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IValueConverterSelector, SingleValueObjectConverterSelector>();
    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Application.DataAccessLayer;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Persistence.Contexts;

public class NbpDbContext : DbContext, INbpDbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<ExchangeRateSnapshot> ExchangeRangeSnapshots { get; set; }
    
    public NbpDbContext(DbContextOptions<NbpDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
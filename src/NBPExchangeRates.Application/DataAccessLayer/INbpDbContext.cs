using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Application.DataAccessLayer;

public interface INbpDbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    DbSet<ExchangeRateSnapshot> ExchangeRangeSnapshots { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBPExchangeRates.Application.DataAccessLayer;
using NBPExchangeRates.Persistence.Contexts;

namespace NBPExchangeRates.Persistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NbpDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("NpbConnection")));

        services.AddScoped<INbpDbContext, NbpDbContext>();

        return services;
    }
}
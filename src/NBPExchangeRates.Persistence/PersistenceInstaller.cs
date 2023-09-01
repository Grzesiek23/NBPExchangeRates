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
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var connectionString = configuration.GetConnectionString("NbpConnection");
        
        if (environment == "PRODUCTION") connectionString = Environment.GetEnvironmentVariable("NBP_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(connectionString);
        
        services.AddDbContext<NbpDbContext>(options => options.UseSqlServer(connectionString));
        
        services.AddScoped<INbpDbContext, NbpDbContext>();

        return services;
    }
}
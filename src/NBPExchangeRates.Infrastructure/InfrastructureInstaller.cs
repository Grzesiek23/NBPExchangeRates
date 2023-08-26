using Microsoft.Extensions.DependencyInjection;
using NBPExchangeRates.Application.Abstractions;
using NBPExchangeRates.Infrastructure.DateTime;

namespace NBPExchangeRates.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        
        return services;
    }
}
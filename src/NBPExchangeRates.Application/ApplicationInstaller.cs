using Microsoft.Extensions.DependencyInjection;

namespace NBPExchangeRates.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationInstaller).Assembly));
        
        return services;
    }
}
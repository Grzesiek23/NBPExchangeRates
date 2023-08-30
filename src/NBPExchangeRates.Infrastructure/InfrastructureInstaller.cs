using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBPExchangeRates.Application.Abstractions;
using NBPExchangeRates.Application.Services;
using NBPExchangeRates.Infrastructure.Cache;
using NBPExchangeRates.Infrastructure.Configuration;
using NBPExchangeRates.Infrastructure.Constants;
using NBPExchangeRates.Infrastructure.DateTime;
using NBPExchangeRates.Infrastructure.NBP;

namespace NBPExchangeRates.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NbpApiConfiguration>(configuration.GetSection(ConfigurationConstants.NbpApiService));
        services.AddMemoryCache();
        services.AddScoped<NbpCacheHandler>();

        services.AddHttpClient<INbpApiService, NbpApiService>(HttpClientConstants.NbpClient, client =>
        {
            var nbpConfiguration = configuration.GetSection(ConfigurationConstants.NbpApiService).Get<NbpApiConfiguration>();
            if (nbpConfiguration is null)
                throw new ArgumentNullException(nameof(nbpConfiguration));

            client.BaseAddress = new Uri(nbpConfiguration.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        })
            .AddHttpMessageHandler<NbpCacheHandler>();

        services.AddSingleton<IClock, Clock>();
        services.AddScoped<INbpApiService, NbpApiService>();
        
        return services;
    }
}
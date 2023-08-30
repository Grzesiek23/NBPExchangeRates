using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NBPExchangeRates.Infrastructure.Configuration;

namespace NBPExchangeRates.Infrastructure.Cache;

public class NbpCacheHandler : DelegatingHandler
{
    private readonly IMemoryCache _memoryCache;
    private readonly NbpApiConfiguration _configuration;

    public NbpCacheHandler(IMemoryCache memoryCache, IOptions<NbpApiConfiguration> configuration)
    {
        _memoryCache = memoryCache;
        _configuration = configuration.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var routeString = request.RequestUri?.ToString() ?? string.Empty;
        var cacheEntry = routeString.Split('/')[^2];
        var cacheKey = routeString.Split('/')[^1];

        var keyEntry = $"{cacheEntry}-{cacheKey}";
        var cached = _memoryCache.Get<string>(keyEntry);
        
        if (cached is not null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(cached)
            };
            return response;
        }
        
        var responseMessage = await base.SendAsync(request, cancellationToken);
        var responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

        _memoryCache.Set(keyEntry, responseContent, TimeSpan.FromMinutes(_configuration.CacheExpirationTimeInMinutes));
        
        return responseMessage;
    }
}
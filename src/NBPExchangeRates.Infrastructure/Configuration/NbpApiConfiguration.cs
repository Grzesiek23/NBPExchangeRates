namespace NBPExchangeRates.Infrastructure.Configuration;

public class NbpApiConfiguration
{
    public string BaseUrl { get; init; }
    public int CacheExpirationTimeInMinutes { get; init; }
}
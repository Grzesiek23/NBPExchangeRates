using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Application.Services;
using NBPExchangeRates.Contracts.Dtos;
using NBPExchangeRates.Infrastructure.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace NBPExchangeRates.Infrastructure.NBP;

public class NbpApiService : INbpApiService
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private const string TablesEndpoint = "exchangerates/tables/";

    public NbpApiService(ILogger logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient(HttpClientConstants.NbpClient);
    }

    public async Task<List<ExchangeRateTableDto?>?> GetTableAsync(NbpTableType tableType)
    {
        _logger.Debug("Starting NbpApiService: GetTable {TableType}", tableType);
        var response = await _httpClient.GetAsync($"{TablesEndpoint}{tableType}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.Error("NbpApiService: GetTable {TableType} returned {StatusCode}", tableType, response.StatusCode);
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(responseContent))
        {
            _logger.Error("NbpApiService: GetTable {TableType} returned empty response", tableType);
            return null;
        }

        var responseDto = JsonConvert.DeserializeObject<List<ExchangeRateTableDto?>?>(responseContent);

        return responseDto;
    }
}
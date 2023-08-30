using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Contracts.Responses;

namespace NBPExchangeRates.Application.Services;

public interface INbpApiService
{
    Task<List<ExchangeRateTableResponse?>?> GetTableAsync(NbpTableType tableType);
}
using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Contracts.Dtos;

namespace NBPExchangeRates.Application.Services;

public interface INbpApiService
{
    Task<List<ExchangeRateTableDto?>?> GetTableAsync(NbpTableType tableType);
}
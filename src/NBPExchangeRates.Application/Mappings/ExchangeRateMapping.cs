using NBPExchangeRates.Contracts.Dtos;
using NBPExchangeRates.Contracts.Responses;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Application.Mappings;

public static class ExchangeRateMapping
{
    public static ExchangeRate AsEntity(this ExchangeRateResponse response)
    {
        return new ExchangeRate
        {
            Currency = new Currency
            {
                Name = response.Currency,
                Code = response.Code
            },
            Mid = (decimal?)response.Mid,
            Bid = (decimal?)response.Bid,
            Ask = (decimal?)response.Ask
        };
    }

    public static ExchangeRateDto AsDto(this ExchangeRate entity)
    {
        return new ExchangeRateDto
        {
            Id = entity.Id,
            CurrencyId = entity.CurrencyId,
            ExchangeRateSnapshotId = entity.ExchangeRateSnapshotId,
            Mid = entity.Mid,
            Bid = entity.Bid,
            Ask = entity.Ask,
            Currency = entity.Currency?.AsDto(),
            ExchangeRateSnapshot = entity.ExchangeRateSnapshot?.AsDtoWithoutNested()
        };
    }
    
    public static ExchangeRateDto AsDtoWithoutNested(this ExchangeRate entity)
    {
        return new ExchangeRateDto
        {
            Id = entity.Id,
            CurrencyId = entity.CurrencyId,
            ExchangeRateSnapshotId = entity.ExchangeRateSnapshotId,
            Mid = entity.Mid,
            Bid = entity.Bid,
            Ask = entity.Ask,
        };
    }
}
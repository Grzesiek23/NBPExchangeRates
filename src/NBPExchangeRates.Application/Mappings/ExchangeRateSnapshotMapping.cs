using NBPExchangeRates.Contracts.Dtos;
using NBPExchangeRates.Contracts.Responses;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Application.Mappings;

public static class ExchangeRateSnapshotMapping
{
    public static ExchangeRateSnapshot AsEntity(this ExchangeRateTableResponse response)
    {
        return new ExchangeRateSnapshot
        {
            EffectiveDate = response.EffectiveDate,
            Number = response.No,
            Table = response.Table,
            ExchangeRates = response.Rates?.Select(x => x.AsEntity()).ToList()
        };
    }
    
    public static ExchangeRateSnapshotDto AsDto(this ExchangeRateSnapshot entity)
    {
        return new ExchangeRateSnapshotDto
        {
            Id = entity.Id,
            EffectiveDate = entity.EffectiveDate,
            Table = entity.Table,
            Number = entity.Number,
            CreatedAt = entity.CreatedAt,
            ExchangeRates = entity.ExchangeRates?.Select(x => x.AsDto()).ToList()
        };
    }
    
    public static ExchangeRateSnapshotDto AsDtoWithoutNested(this ExchangeRateSnapshot entity)
    {
        return new ExchangeRateSnapshotDto
        {
            Id = entity.Id,
            EffectiveDate = entity.EffectiveDate,
            Table = entity.Table,
            Number = entity.Number,
            CreatedAt = entity.CreatedAt
        };
    }
}
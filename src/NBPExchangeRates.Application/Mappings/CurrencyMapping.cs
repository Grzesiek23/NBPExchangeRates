using NBPExchangeRates.Contracts.Dtos;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Application.Mappings;

public static class CurrencyMapping
{
    public static CurrencyDto AsDto(this Currency entity)
    {
        return new CurrencyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code
        };
    }
}
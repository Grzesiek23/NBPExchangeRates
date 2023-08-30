namespace NBPExchangeRates.Contracts.Dtos;

public class CurrencyDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
namespace NBPExchangeRates.Contracts.Dtos;

public class ExchangeRateTableDto
{
    public string Table { get; set; } = null!;
    public string No { get; set; } = null!;
    public DateTime EffectiveDate { get; set; }
    public List<ExchangeRateDto>? Rates { get; set; }
}
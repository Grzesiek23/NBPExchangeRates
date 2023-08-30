namespace NBPExchangeRates.Contracts.Responses;

public class ExchangeRateTableResponse
{
    public string Table { get; set; } = null!;
    public string No { get; set; } = null!;
    public DateTime EffectiveDate { get; set; }
    public List<ExchangeRateResponse>? Rates { get; set; }
}
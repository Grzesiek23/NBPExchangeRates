namespace NBPExchangeRates.Contracts.Dtos;

public class ExchangeRateDto
{
    public int Id { get; set; }
    public int ExchangeRateSnapshotId { get; set; }
    public int CurrencyId { get; set; }
    public decimal? Mid { get; set; }
    public decimal? Bid { get; set; }
    public decimal? Ask { get; set; }
    public ExchangeRateSnapshotDto? ExchangeRateSnapshot { get; set; }
    public CurrencyDto? Currency { get; set; }
}
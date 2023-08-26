namespace NBPExchangeRates.Domain.Entities;

public class ExchangeRate
{
    public int Id { get; set; }
    public int ExchangeRateSnapshotId { get; set; }
    public int CurrencyId { get; set; }
    public decimal? Mid { get; set; }
    public decimal? Bid { get; set; }
    public decimal? Ask { get; set; }
    public ExchangeRateSnapshot? ExchangeRateSnapshot { get; set; }
    public Currency? Currency { get; set; }
}
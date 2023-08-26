namespace NBPExchangeRates.Domain.Entities;

public class ExchangeRateSnapshot : BaseEntity
{
    public string Table { get; set; } = null!;
    public string Number { get; set; } = null!;
    public DateTime EffectiveDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ICollection<ExchangeRate>? ExchangeRates { get; set; }
}
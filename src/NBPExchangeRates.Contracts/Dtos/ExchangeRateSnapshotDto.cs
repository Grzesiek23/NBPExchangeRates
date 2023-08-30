namespace NBPExchangeRates.Contracts.Dtos;

public class ExchangeRateSnapshotDto : BaseDto
{
    public string Table { get; set; } = null!;
    public string Number { get; set; } = null!;
    public DateTime EffectiveDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ICollection<ExchangeRateDto>? ExchangeRates { get; set; }
}
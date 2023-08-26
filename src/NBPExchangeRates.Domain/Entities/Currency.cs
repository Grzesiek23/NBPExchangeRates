namespace NBPExchangeRates.Domain.Entities;

public class Currency : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
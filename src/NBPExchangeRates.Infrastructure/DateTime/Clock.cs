using NBPExchangeRates.Application.Abstractions;

namespace NBPExchangeRates.Infrastructure.DateTime;

public class Clock : IClock
{
    public DateTimeOffset UtcNow() => DateTimeOffset.UtcNow;
}
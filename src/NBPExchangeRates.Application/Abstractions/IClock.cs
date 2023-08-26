namespace NBPExchangeRates.Application.Abstractions;

public interface IClock
{
    DateTimeOffset UtcNow();
}
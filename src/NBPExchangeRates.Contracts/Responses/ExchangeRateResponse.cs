﻿namespace NBPExchangeRates.Contracts.Responses;

public class ExchangeRateResponse
{
    public string Currency { get; set; } = null!;
    public string Code { get; set; } = null!;
    public double? Mid { get; set; }
    public double? Bid { get; set; }
    public double? Ask { get; set; }
}
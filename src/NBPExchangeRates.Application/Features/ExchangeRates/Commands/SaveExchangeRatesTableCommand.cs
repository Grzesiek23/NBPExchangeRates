using MediatR;
using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Application.Abstractions;
using NBPExchangeRates.Application.DataAccessLayer;
using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Application.Mappings;
using NBPExchangeRates.Application.Services;

namespace NBPExchangeRates.Application.Features.ExchangeRates.Commands;

public class SaveExchangeRatesTableCommand : IRequest<int>
{
    public NbpTableType TableType { get; set; }
}

public class SaveExchangeRatesTableHandler : IRequestHandler<SaveExchangeRatesTableCommand, int>
{
    private readonly INbpDbContext _context;
    private readonly INbpApiService _nbpApiService;
    private readonly IClock _clock;

    public SaveExchangeRatesTableHandler(INbpDbContext context, INbpApiService nbpApiService, IClock clock)
    {
        _context = context;
        _nbpApiService = nbpApiService;
        _clock = clock;
    }

    public async Task<int> Handle(SaveExchangeRatesTableCommand request, CancellationToken cancellationToken)
    {
        var apiRequest = await _nbpApiService.GetTableAsync(request.TableType);

        if(apiRequest == null || !apiRequest.Any())
            throw new Exception("No data to save");

        var existingCurrencies = await _context.Currencies.ToListAsync(cancellationToken);

        var snapshot = apiRequest.First()!.AsEntity();
        
        if(snapshot.ExchangeRates is null || !snapshot.ExchangeRates.Any())
            throw new Exception("No data to save");
        
        foreach (var rate in snapshot.ExchangeRates)
        {
            var existingCurrency = existingCurrencies.FirstOrDefault(c => c.Code == rate.Currency?.Code);
        
            if (existingCurrency != null)
            {
                rate.CurrencyId = existingCurrency.Id;
                rate.Currency = null;
            }
            else
            {
                existingCurrencies.Add(rate.Currency!);
                await _context.Currencies.AddAsync(rate.Currency!, cancellationToken);
            }
        }

        snapshot.CreatedAt = _clock.UtcNow();
        
        var result = await _context.ExchangeRangeSnapshots.AddAsync(snapshot, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity.Id;
    }
}
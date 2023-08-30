using MediatR;
using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Application.DataAccessLayer;
using NBPExchangeRates.Application.Mappings;
using NBPExchangeRates.Contracts.Dtos;

namespace NBPExchangeRates.Application.Features.ExchangeRates.Queries;

public class GetExchangeRatesTableByIdQuery : IRequest<ExchangeRateSnapshotDto>
{
    public int Id { get; init; }
}

public class GetExchangeRatesTableByIdHandler : IRequestHandler<GetExchangeRatesTableByIdQuery, ExchangeRateSnapshotDto>
{
    private readonly INbpDbContext _context;

    public GetExchangeRatesTableByIdHandler(INbpDbContext context)
    {
        _context = context;
    }

    public async Task<ExchangeRateSnapshotDto> Handle(GetExchangeRatesTableByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = await _context.ExchangeRangeSnapshots.Include(p => p.ExchangeRates)
            .ThenInclude(p => p.Currency)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        if (query is null)
            throw new Exception("Not found");

        var result = query.AsDto();

        return result;
    }
}
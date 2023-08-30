using MediatR;
using Microsoft.AspNetCore.Mvc;
using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Application.Features.ExchangeRates.Commands;
using NBPExchangeRates.Application.Features.ExchangeRates.Queries;
using NBPExchangeRates.WebApp.ViewModels;

namespace NBPExchangeRates.WebApp.Controllers;

public class ExchangeRatesController : Controller
{
    private readonly IMediator _mediator;

    public ExchangeRatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoadTableDataAjax(string tableTypeString)
    {
        try
        {
            if (!Enum.TryParse<NbpTableType>(tableTypeString, out var tableType))
                throw new ArgumentException($"Invalid table type: {tableTypeString}");

            var result = await _mediator.Send(new SaveExchangeRatesTableCommand {TableType = tableType});

            var query = await _mediator.Send(new GetExchangeRatesTableByIdQuery {Id = result});

            return PartialView("_SnapshotPartial", new ExchangeRateViewModel {ExchangeRateSnapshotDto = query});
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> LoadTableDataForm(string tableTypeString)
    {
        var result = await LoadTable(tableTypeString);

        return RedirectToAction("Snapshots", "ExchangeRates", new {id = result});
    }

    private async Task<int> LoadTable(string tableTypeString)
    {
        if (!Enum.TryParse<NbpTableType>(tableTypeString, out var tableType))
            throw new ArgumentException($"Invalid table type: {tableTypeString}");

        var result = await _mediator.Send(new SaveExchangeRatesTableCommand {TableType = tableType});
        return result;
    }
    
    [HttpGet]
    [Route("ExchangeRates/Snapshots/{id:int}")]
    public async Task<IActionResult> Snapshots(int id)
    {
        var result = await _mediator.Send(new GetExchangeRatesTableByIdQuery {Id = id});

        return View("Snapshot", new ExchangeRateViewModel { ExchangeRateSnapshotDto = result});
    }
}
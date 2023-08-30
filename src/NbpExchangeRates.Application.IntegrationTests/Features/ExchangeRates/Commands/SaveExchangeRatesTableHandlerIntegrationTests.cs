using Microsoft.EntityFrameworkCore;
using Moq;
using NBPExchangeRates.Application.Abstractions;
using NBPExchangeRates.Application.Enums;
using NBPExchangeRates.Application.Features.ExchangeRates.Commands;
using NbpExchangeRates.Application.IntegrationTests.Common;
using NBPExchangeRates.Application.Services;
using NBPExchangeRates.Contracts.Responses;

namespace NbpExchangeRates.Application.IntegrationTests.Features.ExchangeRates.Commands;

[TestFixture]
public class SaveExchangeRatesTableHandlerIntegrationTests : FakeContextFixture
{
    private Mock<INbpApiService> _nbpApiService = null!;
    private Mock<IClock> _clock = null!;

    [SetUp]
    public void SetUp()
    {
        InitContext();
        _nbpApiService = new Mock<INbpApiService>();
        _clock = new Mock<IClock>();
    }
    
    [Test]
    public async Task Handle_ValidTableType_ReturnsExchangeRateSnapshotId()
    {
        // Arrange
        var handler = new SaveExchangeRatesTableHandler(Context, _nbpApiService.Object, _clock.Object);
        var query = new SaveExchangeRatesTableCommand { TableType = NbpTableType.A };
        var dateTimeNow = DateTime.Now;
        _clock.Setup(x => x.UtcNow()).Returns(dateTimeNow);
        _nbpApiService.Setup(x => x.GetTableAsync(It.IsAny<NbpTableType>())).ReturnsAsync(new List<ExchangeRateTableResponse>
        {
            new ()
            {
                No = "1/2021",
                EffectiveDate = DateTime.Now,
                Table = "A",
                Rates = new List<ExchangeRateResponse>
                {
                    new ()
                    {
                        Currency = "dolar amerykański",
                        Code = "USD",
                        Mid = 1.2345
                    },
                    new ()
                    {
                        Currency = "euro",
                        Code = "EUR",
                        Mid = 0.2345
                    }
                }
            }
        });
        _clock.Setup(x => x.UtcNow()).Returns(DateTime.Now);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        var check = await Context.ExchangeRangeSnapshots.FirstOrDefaultAsync(x => x.Id == 1);
        check.Should().NotBeNull();
        check!.Number.Should().Be("1/2021");
        check.EffectiveDate.Should().BeCloseTo(dateTimeNow, TimeSpan.FromSeconds(1));
    }
    
    [Test]
    public void Handle_EmptyRatesReceived_ThrowsException()
    {
        // Arrange
        var handler = new SaveExchangeRatesTableHandler(Context, _nbpApiService.Object, _clock.Object);
        var query = new SaveExchangeRatesTableCommand { TableType = NbpTableType.C };
        _nbpApiService.Setup(x => x.GetTableAsync(It.IsAny<NbpTableType>()))
            .ReturnsAsync(new List<ExchangeRateTableResponse>());
        
        // Act
        var act = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("No data to save");
    }
}
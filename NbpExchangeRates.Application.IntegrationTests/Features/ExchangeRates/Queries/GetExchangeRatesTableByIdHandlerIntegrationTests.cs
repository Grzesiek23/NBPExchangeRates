using NBPExchangeRates.Application.Features.ExchangeRates.Queries;
using NbpExchangeRates.Application.IntegrationTests.Common;

namespace NbpExchangeRates.Application.IntegrationTests.Features.ExchangeRates.Queries;

[TestFixture]
public class GetExchangeRatesTableByIdHandlerIntegrationTests : FakeContextFixture
{
    [SetUp]
    public async Task SetUp()
    {
        InitContext();
        Context.ExchangeRangeSnapshots.Add( new ()
        {
            Id = 1,
            CreatedAt = DateTime.Now,
            Number = "1/2021",
            EffectiveDate = DateTime.Now,
            Table = "A",
            ExchangeRates = new List<ExchangeRate>
            {
                new ()
                {
                    Id = 1,
                    Currency = new Currency
                    {
                        Id = 1,
                        Name = "dolar amerykański",
                        Code = "USD"
                    },
                    ExchangeRateSnapshotId = 1,
                    Mid = 1.2345m
                },
                new ()
                {
                    Id = 2,
                    Currency = new Currency
                    {
                        Id = 2,
                        Name = "euro",
                        Code = "EUR"
                    },
                    ExchangeRateSnapshotId = 1,
                    Mid = 1.2345m
                }
            }
        });
        await Context.SaveChangesAsync();
    }
    
    [Test]
    public async Task Handle_ValidId_ReturnsExchangeRateSnapshotDto()
    {
        // Arrange
        var handler = new GetExchangeRatesTableByIdHandler(Context);
        var query = new GetExchangeRatesTableByIdQuery { Id = 1 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<ExchangeRateSnapshotDto>();
        result.ExchangeRates.Should().HaveCount(2);
    }
}
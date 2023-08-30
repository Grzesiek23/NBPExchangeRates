using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NBPExchangeRates.Persistence.Contexts;

namespace NbpExchangeRates.Application.IntegrationTests.Common;

public class FakeContextFixture : IAsyncDisposable
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private SqliteConnection _connection;

    protected NbpDbContext Context;

    protected void InitContext()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _connection.Open();

        var options = new DbContextOptionsBuilder<NbpDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new NbpDbContext(options);
        Context.Database.EnsureCreated();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.DisposeAsync();
        await _connection.CloseAsync();
    }
}
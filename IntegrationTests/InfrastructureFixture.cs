using Features.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Application.IntegrationTest;

public class InfrastructureFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlServerContainer = new MsSqlBuilder().Build();
    public AppDbContext TestDbContext { get; private set; }

    public async Task InitializeAsync()
    {
        await _sqlServerContainer.StartAsync()
            .ConfigureAwait(false);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_sqlServerContainer.GetConnectionString(), m => m.MigrationsAssembly("Migrations"))
            .Options;

        Console.WriteLine("Connection string {0}", _sqlServerContainer.GetConnectionString());

        TestDbContext = new AppDbContext(options);
        await TestDbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _sqlServerContainer.DisposeAsync();
    }
}
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Pingo.Identity.Service;
using Pingo.Identity.Service.Entity.Options;
using Respawn;
using Testcontainers.PostgreSql;

namespace Pingo.Identity.ComponentTests;

public sealed class WebAppFactoryFixture : IAsyncLifetime
{
    private Respawner _respawner = null!;
    private PostgreSqlContainer _container = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
             .WithImage("postgres:latest")
             .WithCleanUp(true)
             .Build();

        await _container.StartAsync();

        var factory = new WebApplicationFactory<WebApi.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Configure<DataBaseSettings>(opts =>
                        opts.DefaultConnection = _container.GetConnectionString());
                    services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
                    services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
                });
            });

        using var scope = factory.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
        await seeder.SeedAsync();

        await using var conn = new NpgsqlConnection(_container.GetConnectionString());
        await conn.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
        });
    }

    public HttpClient CreateClient()
    {
        var factory = new WebApplicationFactory<WebApi.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Configure<DataBaseSettings>(opts =>
                        opts.DefaultConnection = _container.GetConnectionString());
                    services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
                    services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
                });
            });

        return factory.CreateClient();
    }

    public async Task ResetAsync()
    {
        await using var conn = new NpgsqlConnection(_container.GetConnectionString());
        await conn.OpenAsync();
        await conn.ExecuteAsync("DROP TYPE IF EXISTS your_type_name CASCADE;");
        await _respawner.ResetAsync(conn);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

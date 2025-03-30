using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Extensions;
using Testcontainers.PostgreSql;

namespace Pingo.Identity.ComponentTests;

public sealed class WebAppFactoryFixture
{
    private readonly WebApplicationFactory<WebApi.Program> _factory;

    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithCleanUp(true)
        .Build();

    private static readonly string ConnectionString =
        "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

    public WebAppFactoryFixture()
    {
        _ = StartContainerAsync();

        _factory = new WebApplicationFactory<WebApi.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddIdentity();
                    services.Configure<DataBaseSettings>(opts =>
                        opts.DefaultConnection = ConnectionString);
                });
            });
    }

    internal WebApplicationFactory<WebApi.Program> Factory => _factory;

    private async Task StartContainerAsync() => await _container.StartAsync();

    public HttpClient CreateClient() => _factory.CreateClient();

    public static async Task ResetAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        await new NpgsqlCommand("""TRUNCATE "User", "UserCredentials", "RefreshTokens" CASCADE""", connection)
            .ExecuteNonQueryAsync();
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Extensions;
using Testcontainers.PostgreSql;

namespace IdentityComponentTests;

public sealed class WebAppFactoryFixture
{
    private readonly WebApplicationFactory<Pingo.Identity.WebApi.Program> _factory;

    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithCleanUp(true)
        .Build();

    public WebAppFactoryFixture()
    {
        _ = StartContainerAsync();

        _factory = new WebApplicationFactory<Pingo.Identity.WebApi.Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddIdentity();
                services.Configure<DataBaseSettings>(opts =>
                    opts.DefaultConnection = PostgresContainer.ConnectionString);
                services.AddHostedService<TestDatabaseMigrator>();
            });
        });
    }

    private async Task StartContainerAsync() => await _container.StartAsync();

    public HttpClient CreateClient() => _factory.CreateClient();

    public static async Task ResetAsync()
    {
        await using var connection = new NpgsqlConnection(PostgresContainer.ConnectionString);
        await connection.OpenAsync();
        await new NpgsqlCommand("TRUNCATE users, user_credentials, refresh_tokens CASCADE", connection)
            .ExecuteNonQueryAsync();
    }
}

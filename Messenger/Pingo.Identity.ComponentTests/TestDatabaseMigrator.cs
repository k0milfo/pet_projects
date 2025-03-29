using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Pingo.Identity.Service;

namespace Pingo.Identity.ComponentTests;

internal sealed class TestDatabaseMigrator(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbConnection = scope.ServiceProvider.GetRequiredService<IDatabaseConnectionFactory>();

        await PostgresContainer.CreateDbAsync();
        await using var connection = dbConnection.GetDbConnection();
        await connection.OpenAsync(stoppingToken);

        await ExecuteSqlAsync(SqlQueries.CreateTableUser, connection, stoppingToken);
        await ExecuteSqlAsync(SqlQueries.CreateTableUserCredentials, connection, stoppingToken);
        await ExecuteSqlAsync(SqlQueries.CreateTableRefreshTokens, connection, stoppingToken);
    }

    private static async Task ExecuteSqlAsync(string sql, NpgsqlConnection connection, CancellationToken stoppingToken)
    {
        await using var command = new NpgsqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync(stoppingToken);
    }
}

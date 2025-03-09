using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Pingo.Identity.Service.Service;

public sealed class DataBaseMigrator(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbConnection = scope.ServiceProvider.GetRequiredService<IDatabaseConnection>();

        await dbConnection.CreateDbAsync();
        await using var connection = dbConnection.GetDbConnection();
        await connection.OpenAsync(stoppingToken);

        await ExecuteSqlAsync(Task.FromResult(await ReadSqlFileAsync("SqlQueries", "CreateTableUser.sql", stoppingToken)), connection, stoppingToken);
        await ExecuteSqlAsync(Task.FromResult(await ReadSqlFileAsync("SqlQueries", "CreateTableIUser_Credentials.sql", stoppingToken)), connection, stoppingToken);
    }

    private static Task<string> ReadSqlFileAsync(string directory, string file, CancellationToken stoppingToken)
    {
        var userTablePath = Path.Combine(Directory.GetCurrentDirectory(), directory, file);
        return File.ReadAllTextAsync(userTablePath, stoppingToken);
    }

    private static async Task ExecuteSqlAsync(Task<string> sql, NpgsqlConnection connection, CancellationToken stoppingToken)
    {
        await using var command = new NpgsqlCommand(await sql, connection);
        await command.ExecuteNonQueryAsync(stoppingToken);
    }
}

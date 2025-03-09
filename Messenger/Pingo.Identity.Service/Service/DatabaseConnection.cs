using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Pingo.Identity.Service.Service;

internal sealed class DatabaseConnection(IConfiguration configuration) : IDatabaseConnection
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task CreateDbAsync()
    {
        var masterConnectionString = _connectionString;
        var checkDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "CheckingDatabaseCreation.sql");
        var checkDatabaseQuery = File.ReadAllTextAsync(checkDatabasePath);
        var createDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "CreateDatabase.sql");
        var createDatabaseQuery = File.ReadAllTextAsync(createDatabasePath);

        await using var masterConnection = new NpgsqlConnection(masterConnectionString);
        await masterConnection.OpenAsync();
        await using var command = new NpgsqlCommand(await checkDatabaseQuery, masterConnection);
        var exists = command.ExecuteNonQueryAsync() != null;
        if (!exists)
        {
            await using var dbCommand = new NpgsqlCommand(await createDatabaseQuery, masterConnection);
            await dbCommand.ExecuteNonQueryAsync();
        }
    }

    public NpgsqlConnection GetDbConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public async Task ExecuteCommandAsync(string sql, Func<NpgsqlCommand, Task> action)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(sql, connection);
        await action(command);
    }
}

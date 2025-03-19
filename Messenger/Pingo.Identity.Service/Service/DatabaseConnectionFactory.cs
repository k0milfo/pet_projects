using Microsoft.Extensions.Options;
using Npgsql;
using Pingo.Identity.Service.Entity;

namespace Pingo.Identity.Service.Service;

internal sealed class DatabaseConnectionFactory(IOptions<DataBaseSettings> databaseSettings) : IDatabaseConnectionFactory
{
    public async Task CreateDbAsync()
    {
        await using var masterConnection = new NpgsqlConnection(databaseSettings.Value.DefaultConnection);
        await masterConnection.OpenAsync();
        await using var command = new NpgsqlCommand(SqlQueries.CheckingDatabaseCreation, masterConnection);
        var exists = command.ExecuteNonQueryAsync() != null;
        if (!exists)
        {
            await using var dbCommand = new NpgsqlCommand(SqlQueries.CreateDatabase, masterConnection);
            await dbCommand.ExecuteNonQueryAsync();
        }
    }

    public NpgsqlConnection GetDbConnection()
    {
        return new NpgsqlConnection(databaseSettings.Value.DefaultConnection);
    }
}

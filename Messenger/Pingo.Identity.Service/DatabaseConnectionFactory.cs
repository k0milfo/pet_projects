using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Pingo.Identity.Service.Entity.Options;

namespace Pingo.Identity.Service;

internal sealed class DatabaseConnectionFactory(IOptions<DataBaseSettings> databaseSettings) : IDatabaseConnectionFactory
{
    public async Task CreateDbAsync()
    {
        await using var masterConnection = new NpgsqlConnection(databaseSettings.Value.DefaultConnection);
        await masterConnection.OpenAsync();

        if (await masterConnection.QueryFirstOrDefaultAsync<int>(SqlQueries.CheckingDatabaseCreation) <= 0)
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

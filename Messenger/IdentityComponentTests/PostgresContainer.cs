using Dapper;
using DotNet.Testcontainers.Builders;
using Npgsql;
using Pingo.Identity.Service;

namespace IdentityComponentTests;

internal static class PostgresContainer
{
    public static string ConnectionString =
        "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

    public static async Task TestContainerBuilderAsync()
    {
        var container = new ContainerBuilder()
            .WithImage("postgres:latest")
            .WithEnvironment("POSTGRES_PASSWORD", "postgres")
            .WithEnvironment("POSTGRES_USER", "postgres")
            .WithEnvironment("POSTGRES_DB", "testdb")
            .WithPortBinding(5432, assignRandomHostPort: true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
        await container.StartAsync();
    }

    public static async Task CreateDbAsync()
    {
        await using var masterConnection = new NpgsqlConnection(ConnectionString);
        await masterConnection.OpenAsync();

        if (await masterConnection.QueryFirstOrDefaultAsync<int>(SqlQueries.CheckingDatabaseCreation) <= 0)
        {
            await using var dbCommand = new NpgsqlCommand(SqlQueries.CreateDatabase, masterConnection);
            await dbCommand.ExecuteNonQueryAsync();
        }
    }
}

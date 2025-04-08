using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Pingo.Identity.Service;

internal sealed class DatabaseSeeder(IServiceProvider serviceProvider) : IDatabaseSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var connectionFactory = scope.ServiceProvider.GetRequiredService<IDatabaseConnectionFactory>();

        await using var connection = connectionFactory.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        const string dropAndRecreateSchema = """
                                                 DROP SCHEMA public CASCADE;
                                                 CREATE SCHEMA public;
                                             """;

        await ExecuteSqlAsync(dropAndRecreateSchema, connection, cancellationToken);

        await ExecuteSqlAsync(SqlQueries.CreateTableUser, connection, cancellationToken);
        await ExecuteSqlAsync(SqlQueries.CreateTableUserCredentials, connection, cancellationToken);
        await ExecuteSqlAsync(SqlQueries.CreateTableRefreshTokens, connection, cancellationToken);
    }

    private static async Task ExecuteSqlAsync(string sql, NpgsqlConnection connection, CancellationToken ct)
    {
        await using var cmd = new NpgsqlCommand(sql, connection);
        await cmd.ExecuteNonQueryAsync(ct);
    }
}

using Npgsql;

namespace Pingo.Identity.Service;

internal interface IDatabaseConnectionFactory
{
    Task CreateDbAsync();

    NpgsqlConnection GetDbConnection();
}

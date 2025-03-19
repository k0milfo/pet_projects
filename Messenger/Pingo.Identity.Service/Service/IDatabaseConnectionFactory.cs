using Npgsql;

namespace Pingo.Identity.Service.Service;

public interface IDatabaseConnectionFactory
{
    Task CreateDbAsync();

    NpgsqlConnection GetDbConnection();
}

using Npgsql;

namespace Pingo.Identity.Service.Service;

public interface IDatabaseConnection
{
    Task CreateDbAsync();

    NpgsqlConnection GetDbConnection();

    Task ExecuteCommandAsync(string sql, Func<NpgsqlCommand, Task> action);
}

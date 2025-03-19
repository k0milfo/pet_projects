using Dapper;
using Pingo.Identity.Service.Entity;
using Pingo.Identity.Service.Service.Interface;

namespace Pingo.Identity.Service.Service.Repositories;

internal sealed class IdentityRepository(IDatabaseConnectionFactory dbConnectionFactory) : IIdentityRepository
{
    public async Task InsertAsync(User entity)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.InsertUserMetadata, new
        {
            id = entity.Id,
            registration_time = entity.RegisteredAt,
        });

        await connection.ExecuteAsync(SqlQueries.InsertUserCredentials, new
        {
            user_id = entity.Id,
            Email = entity.Email,
            HashPassword = entity.PasswordHash,
            Salt = entity.Salt,
        });
    }

    public async Task<User?> GetAsync(string email)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(SqlQueries.GetUser, new
        {
            Email = email,
        }) ?? null;
    }
}

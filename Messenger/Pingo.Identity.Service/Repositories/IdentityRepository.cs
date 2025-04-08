using CSharpFunctionalExtensions;
using Dapper;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Repositories;

internal sealed class IdentityRepository(IDatabaseConnectionFactory dbConnectionFactory) : IIdentityRepository
{
    public async Task InsertUserAsync(User entity)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.InsertUserMetadata, new
        {
            entity.Id,
            entity.RegisteredAt,
            entity.Email,
        });

        await connection.ExecuteAsync(SqlQueries.InsertUserCredentials, new
        {
            UserId = entity.Id,
            entity.Email,
            entity.PasswordHash,
            entity.Salt,
        });
    }

    public async Task<Result<User?>> GetUserAsync(string email)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(SqlQueries.GetUser, new
        {
            Email = email,
        });
    }

    public async Task<TokenData?> GetRefreshTokenAsync(Guid? token)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        return await connection.QueryFirstOrDefaultAsync<TokenData>(SqlQueries.GetToken, new
        {
            Token = token,
        });
    }

    public async Task InsertRefreshTokenAsync(TokenData data)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.InsertRefreshToken, new
        {
            data.Token,
            data.ExpirationTime,
        });
    }

    public async Task DeleteRefreshTokenAsync(TokenData data)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.DeleteInvalidateRefreshToken, new
        {
            data.Token,
            data.ExpirationTime,
        });
    }
}

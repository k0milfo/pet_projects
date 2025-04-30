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

    public async Task<User?> GetUserAsync(string email)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        var entity = await connection.QueryFirstOrDefaultAsync<UserEntity>(SqlQueries.GetUser, new
        {
            Email = email,
        });

        return entity is null
            ? null
            : new User(entity.Id, new DateTimeOffset(entity.RegisteredAt), entity.Email, entity.PasswordHash, entity.Salt);
    }

    public async Task<TokenData?> GetRefreshTokenAsync(Guid? token)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        var entity = await connection.QueryFirstOrDefaultAsync<TokenDataEntity>(SqlQueries.GetToken, new
        {
            Token = token,
        });

        return entity is null
            ? null
            : new TokenData(entity.Token, entity.ExpirationTime);
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

    public async Task DeleteRefreshTokenAsync(Guid? refreshToken)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.DeleteInvalidateRefreshToken, new
        {
            Token = refreshToken,
        });
    }

    public async Task<IReadOnlyList<TokenData>> GetInvalidRefreshTokensAsync()
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        var expiredTokens = await connection.QueryAsync<TokenDataEntity>(SqlQueries.GetExpiredTokens);
        return expiredTokens
            .Select(i => new TokenData(i.Token, i.ExpirationTime))
            .ToArray();
    }
}

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

        await connection.ExecuteAsync(SqlQueries.InsertRefreshToken, new
        {
            Email = entity.Email,
            entity.Token,
        });
    }

    public async Task<Result<User?>> GetUserAsync(string email)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        return Result.Success(await connection.QueryFirstOrDefaultAsync<User>(SqlQueries.GetUser, new
        {
            Email = email,
        }));
    }

    public async Task InsertRefreshTokenAsync(RefreshTokenRequest request)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.InsertRefreshToken, new
        {
            request.Email,
            request.Token,
        });
    }

    public async Task DeleteRefreshTokenAsync(RefreshTokenRequest request)
    {
        await using var connection = dbConnectionFactory.GetDbConnection();

        await connection.ExecuteAsync(SqlQueries.DeleteInvalidateRefreshToken, new
        {
            request.Email,
            request.Token,
        });
    }
}

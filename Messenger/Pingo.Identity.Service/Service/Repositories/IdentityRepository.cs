using System.Globalization;
using AutoMapper;
using DataTransferObject;
using Pingo.Identity.Service.Entity;
using Pingo.Identity.Service.Service.Interface;

namespace Pingo.Identity.Service.Service.Implementations;

internal sealed class IdentityRepository(IDatabaseConnection dbConnection, IMapper mapper) : IIdentityRepository<EntityDto>
{
    public async Task InsertAsync(EntityDto entity)
    {
        var userSql = "INSERT INTO \"User\" (id, registration_time) VALUES (@id, @registration_time)";
        var credentialsSql = "INSERT INTO \"UserCredentials\" (user_id, Email, HashPassword, Salt) VALUES (@user_id, @Email, @HashPassword, @Salt)";
        var user = mapper.Map<User>(entity);

        await dbConnection.ExecuteCommandAsync(userSql, async command =>
        {
            command.Parameters.AddWithValue("id", user.Id);
            command.Parameters.AddWithValue("registration_time", user.SentAt);
            await command.ExecuteNonQueryAsync();
        });
        await dbConnection.ExecuteCommandAsync(credentialsSql, async command =>
        {
            command.Parameters.AddWithValue("user_id", user.Id);
            command.Parameters.AddWithValue("Email", user.Email);
            command.Parameters.AddWithValue("HashPassword", user.HashPassword);
            command.Parameters.AddWithValue("Salt", user.Salt);
            await command.ExecuteNonQueryAsync();
        });
    }

    public async Task<EntityDto?> GetAsync(string email)
    {
        User user = null!;
        var sqlPath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "GetUser.sql");
        var sql = File.ReadAllTextAsync(sqlPath);

        await dbConnection.ExecuteCommandAsync(await sql, async command =>
        {
            command.Parameters.AddWithValue("@Email", email);
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var user_id = reader["id"].ToString()!;
                var email = reader["Email"].ToString()!;
                var registration_time = reader["registration_time"].ToString()!;
                var hashPassword = reader["HashPassword"].ToString()!;
                var salt = reader["Salt"].ToString()!;
                user = new User(Guid.Parse(user_id), DateTimeOffset.Parse(registration_time, CultureInfo.InvariantCulture), email, hashPassword, salt);
            }
        });
        if (user != null)
        {
            return mapper.Map<EntityDto>(user);
        }

        return null;
    }
}

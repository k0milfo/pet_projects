namespace Pingo.Identity.Service;

internal static class SqlQueries
{
    public const string CheckingDatabaseCreation = """
          SELECT 1 FROM pg_database WHERE datname = 'postgres';
    """;

    public const string CreateDatabase = """
          CREATE DATABASE postgres WITH OWNER = postgres 
          ENCODING = 'UTF8' CONNECTION LIMIT = -1;
    """;

    public const string CreateTableUserCredentials = """
          CREATE TABLE IF NOT EXISTS "UserCredentials"
          (UserId UUID PRIMARY KEY, Email TEXT UNIQUE, PasswordHash TEXT NOT NULL, Salt TEXT NOT NULL, 
          FOREIGN KEY (UserId, Email) REFERENCES "User"(Id, Email) ON DELETE CASCADE);
    """;

    public const string CreateTableRefreshTokens = """
          CREATE TABLE IF NOT EXISTS "RefreshTokens"
          (
          Token UUID PRIMARY KEY, ExpirationTime TIMESTAMPTZ NOT NULL
          );
    """;

    public const string CreateTableUser = """
          CREATE TABLE IF NOT EXISTS "User" 
          (
          Id UUID, Email TEXT UNIQUE, RegisteredAt TIMESTAMPTZ NOT NULL, PRIMARY KEY(Id, Email)
          );
    """;

    public const string GetUser = """
          SELECT i.Id, i.RegisteredAt, j.Email, j.PasswordHash, j.Salt
          FROM "User" i
          JOIN "UserCredentials" j ON i.Id = j.UserId
          WHERE j.Email = @Email
    """;

    public const string GetToken = """
          SELECT Token, ExpirationTime
          FROM "RefreshTokens"
          WHERE Token = @Token
    """;

    public const string InsertUserMetadata = """
          INSERT INTO "User" (Id, Email, RegisteredAt) 
          VALUES (@Id, @Email, @RegisteredAt);
    """;

    public const string InsertRefreshToken = """
           INSERT INTO "RefreshTokens" (Token, ExpirationTime)
           VALUES (@Token, @ExpirationTime)
           ON CONFLICT (Token) 
           DO UPDATE SET
           Token = EXCLUDED.Token,
           ExpirationTime = EXCLUDED.ExpirationTime;
    """;

    public const string DeleteInvalidateRefreshToken = """
          DELETE FROM "RefreshTokens"
          WHERE Token = @Token OR Token IS NULL;
    """;

    public const string InsertUserCredentials = """
          INSERT INTO "UserCredentials" 
          (UserId, Email, PasswordHash, Salt) VALUES (@UserId, @Email, @PasswordHash, @Salt)
    """;
}

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
          (
          UserId UUID, 
          Email TEXT UNIQUE, 
          PasswordHash TEXT NOT NULL, 
          Salt TEXT NOT NULL,
          PRIMARY KEY (UserId, Email), 
          FOREIGN KEY (UserId) REFERENCES "User"(Id) ON DELETE CASCADE
          );
    """;

    public const string CreateTableRefreshTokens = """
          CREATE TABLE IF NOT EXISTS "RefreshTokens"
          (
          UserId UUID,
          Token UUID,
          ExpirationTime TIMESTAMPTZ NOT NULL,
          PRIMARY KEY (Token), 
          FOREIGN KEY (UserId) REFERENCES "User"(Id) ON DELETE CASCADE
          );
    """;

    public const string CreateIndexOnRefreshTokens = """
          CREATE INDEX IF NOT EXISTS "IX_RefreshTokens_ExpirationTime" 
          ON "RefreshTokens" (ExpirationTime);
    """;

    public const string CreateTableUser = """
          CREATE TABLE IF NOT EXISTS "User" 
          (
          Id UUID, 
          RegisteredAt TIMESTAMPTZ NOT NULL, 
          PRIMARY KEY(Id)
          );
    """;

    public const string GetUser = """
          SELECT i.Id, i.RegisteredAt, j.Email, j.PasswordHash, j.Salt
          FROM "User" i
          JOIN "UserCredentials" j ON i.Id = j.UserId
          WHERE j.Email = @Email
    """;

    public const string GetToken = """
          SELECT Token AS token, UserId AS userId, ExpirationTime AS expirationTime
          FROM "RefreshTokens"
          WHERE Token = @Token AND ExpirationTime > NOW()
   """;

    public const string InsertUserMetadata = """
          INSERT INTO "User" (Id, RegisteredAt) 
          VALUES (@Id, @RegisteredAt);
    """;

    public const string InsertRefreshToken = """
           INSERT INTO "RefreshTokens" (Token, UserId, ExpirationTime)
           VALUES (@Token, @UserId, @ExpirationTime)
           ON CONFLICT (Token) 
           DO UPDATE SET
           Token = EXCLUDED.Token,
           UserId = EXCLUDED.UserId,
           ExpirationTime = EXCLUDED.ExpirationTime;
    """;

    public const string DeleteInvalidateRefreshToken = """
          DELETE FROM "RefreshTokens"
          WHERE Token = @Token OR Token IS NULL;
    """;

    public const string DeleteExpiredRefreshTokens = """
          DELETE FROM "RefreshTokens"
          WHERE ExpirationTime < @DateTime;
    """;

    public const string InsertUserCredentials = """
          INSERT INTO "UserCredentials" 
          (UserId, Email, PasswordHash, Salt) VALUES (@UserId, @Email, @PasswordHash, @Salt);
    """;
}

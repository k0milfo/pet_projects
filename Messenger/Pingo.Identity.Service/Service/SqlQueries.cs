namespace Pingo.Identity.Service.Service;

public static class SqlQueries
{
    public const string CheckingDatabaseCreation = """
          SELECT 1 FROM pg_database WHERE datname = 'postgres';
    """;

    public const string CreateDatabase = """
          CREATE DATABASE postgres WITH OWNER = postgres 
          ENCODING = 'UTF8' CONNECTION LIMIT = -1;
    """;

    public const string CreateTableIUserCredentials = """
          CREATE TABLE IF NOT EXISTS "UserCredentials"
          (user_id UUID PRIMARY KEY, Email TEXT NOT NULL, HashPassword TEXT NOT NULL, Salt TEXT NOT NULL, 
          FOREIGN KEY (user_id) REFERENCES "User"(id) ON DELETE CASCADE);
    """;

    public const string CreateTableUser = """
          CREATE TABLE IF NOT EXISTS "User" 
          (id UUID PRIMARY KEY, registration_time TIMESTAMP WITH TIME ZONE NOT NULL);
    """;

    public const string GetUser = """
          SELECT i.id AS Id, i.registration_time AS RegisteredAt, j.Email, j.HashPassword, j.Salt
          FROM "User" i
          JOIN "UserCredentials" j ON i.id = j.user_id
          WHERE j.Email = @Email;
    """;

    public const string InsertUserMetadata = """
          INSERT INTO "User" (id, registration_time) 
          VALUES (@id, @registration_time);
    """;

    public const string InsertUserCredentials = """
          INSERT INTO "UserCredentials" 
          (user_id, Email, HashPassword, Salt) VALUES (@user_id, @Email, @HashPassword, @Salt)
    """;
}

namespace Pingo.Identity.Service;

internal sealed class PasswordHasher : IPasswordHasher
{
    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public string PasswordHash(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}

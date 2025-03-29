namespace Pingo.Identity.Service;

internal interface IPasswordHasher
{
    string GenerateSalt();

    string PasswordHash(string password, string salt);

    bool VerifyPassword(string password, string hashedPassword);
}

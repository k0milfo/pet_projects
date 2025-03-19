namespace Pingo.Identity.Service.Service;

public interface IPasswordHasher
{
    string GenerateSalt();

    string PasswordHash(string password, string salt);

    bool VerifyPassword(string password, string hashedPassword);
}

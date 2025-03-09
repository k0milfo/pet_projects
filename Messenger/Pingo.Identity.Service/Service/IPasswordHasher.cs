namespace Pingo.Identity.Service.Service;

public interface IPasswordHasher
{
    string GenerateSalt();

    string HashPassword(string password, string salt);

    bool VerifyPassword(string password, string hashedPassword);
}

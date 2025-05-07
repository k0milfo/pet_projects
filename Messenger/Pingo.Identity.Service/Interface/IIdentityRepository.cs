using Pingo.Identity.Service.Entity.Models;

namespace Pingo.Identity.Service.Interface;

internal interface IIdentityRepository
{
    Task InsertUserAsync(User entity);

    Task<User?> GetUserAsync(string email);

    Task InsertRefreshTokenAsync(TokenData data);

    Task DeleteRefreshTokenAsync(Guid refreshToken);

    Task DeleteExpiredRefreshTokensAsync(DateTimeOffset dateTime);

    Task<TokenData?> GetRefreshTokenAsync(Guid token);
}

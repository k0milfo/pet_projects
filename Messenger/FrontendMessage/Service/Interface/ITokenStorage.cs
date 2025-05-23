using FrontendMessage.Entity;

namespace FrontendMessage.Service.Interface;

public interface ITokenStorage
{
    Task SaveAuthDataAsync(TokenResponse tokens, string email);

    Task<string?> GetAccessTokenAsync();

    Task<Guid?> GetRefreshTokenAsync();

    Task<string?> GetEmailAsync();

    Task ClearTokensAsync();
}

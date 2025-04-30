using FrontendMessage.Entity;
using FrontendMessage.Service.Interface;
using Microsoft.JSInterop;

namespace FrontendMessage.Service.Implementations;

internal sealed class TokenStorage(IJSRuntime js) : ITokenStorage
{
    public async Task SaveAuthDataAsync(TokenResponse tokens, string email)
    {
        await js.InvokeVoidAsync("localStorage.setItem", "accessToken", tokens.AccessToken);
        await js.InvokeVoidAsync("localStorage.setItem", "refreshToken", tokens.RefreshToken);
        await js.InvokeVoidAsync("localStorage.setItem", "email", email);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await js.InvokeAsync<string>("localStorage.getItem", "accessToken");
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await js.InvokeAsync<string>("localStorage.getItem", "refreshToken");
    }

    public async Task<string?> GetEmailAsync()
    {
        return await js.InvokeAsync<string>("localStorage.getItem", "email");
    }

    public async Task ClearTokensAsync()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await js.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
        await js.InvokeVoidAsync("localStorage.removeItem", "email");
    }
}

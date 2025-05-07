using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using Blazored.LocalStorage;
using FrontendMessage.Entity;

namespace FrontendMessage.Service.Implementations;

public sealed class AuthenticationHandler(
    IdentityApiClient identityApi, ILocalStorageService storageService)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var accessToken = await storageService.GetItemAsync<string>("accessToken", cancellationToken);

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshToken = await storageService.GetItemAsync<string>("refreshToken", cancellationToken);
            var email = await storageService.GetItemAsync<string>("email", cancellationToken);
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var tokensRequest = new HttpRequestMessage(HttpMethod.Post, "api/identity/refresh")
                {
                    Content = JsonContent.Create(new
                    {
                        Email = email,
                        RefreshToken = refreshToken,
                    }),
                };

                var tokensResponse = await identityApi.Client.SendAsync(tokensRequest, cancellationToken);

                if (tokensResponse.IsSuccessStatusCode)
                {
                    var tokens = await tokensResponse.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
                    await ClearAuthDataAsync();
                    await SaveAuthDataAsync(tokens!.AccessToken, tokens.RefreshToken, email!);
                }

                accessToken = await storageService.GetItemAsync<string>("accessToken", cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                return await identityApi.Client.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }

    private async Task SaveAuthDataAsync(string accessToken, Guid refreshToken, string email)
    {
        await storageService.SetItemAsync("accessToken", accessToken);
        await storageService.SetItemAsync("refreshToken", refreshToken);
        await storageService.SetItemAsync("email", email);
    }

    private async Task ClearAuthDataAsync()
    {
        await storageService.RemoveItemAsync("accessToken");
        await storageService.RemoveItemAsync("refreshToken");
        await storageService.RemoveItemAsync("email");
    }
}

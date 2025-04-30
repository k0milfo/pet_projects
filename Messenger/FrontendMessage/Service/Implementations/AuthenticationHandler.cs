using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FrontendMessage.Entity;
using FrontendMessage.Service.Interface;

namespace FrontendMessage.Service.Implementations;

public sealed class AuthenticationHandler(
    ITokenStorage tokenStorage)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var accessToken = await tokenStorage.GetAccessTokenAsync();

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshToken = await tokenStorage.GetRefreshTokenAsync();
            var email = await tokenStorage.GetEmailAsync();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var tokensRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7294/api/identity/refresh")
                {
                    Content = JsonContent.Create(new
                    {
                        Email = email,
                        RefreshToken = Guid.Parse(refreshToken),
                    }),
                };

                var tokensResponse = await base.SendAsync(tokensRequest, cancellationToken);

                if (tokensResponse.IsSuccessStatusCode)
                {
                    var tokens = await tokensResponse.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
                    await tokenStorage.ClearTokensAsync();
                    await tokenStorage.SaveAuthDataAsync(new TokenResponse(tokens!.AccessToken, tokens.RefreshToken), email!);
                }

                accessToken = await tokenStorage.GetAccessTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}

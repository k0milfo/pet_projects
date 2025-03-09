using System.Security.Claims;
using DataTransferObject;
using Pingo.Identity.Service.Service;
using Pingo.Identity.Service.Service.Interface;
using Pingo.Identity.WebApi.Entity;
using Pingo.Identity.WebApi.Service.Interface;

namespace Pingo.Identity.WebApi.Service.Implementations;

internal sealed class IdentityService(IIdentityRepository<EntityDto> identityRepository, IPasswordHasher hasher, ITokenService token) : IIdentityService<LoginRequest>
{
    public async Task InsertAsync(LoginRequest request)
    {
        var salt = hasher.GenerateSalt();
        if (request is { Password: not null, Email: not null })
        {
            var hashPassword = hasher.HashPassword(request.Password, salt);
            var entity = new EntityDto(Guid.NewGuid(), DateTimeOffset.UtcNow, request.Email, hashPassword, salt);
            await identityRepository.InsertAsync(entity);
        }
    }

    public async Task<bool> GetAsync(LoginRequest request)
    {
        var user = await identityRepository.GetAsync(request.Email!);
        return hasher.VerifyPassword(request.Password!, user?.HashPassword!);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        string accessToken = string.Empty, refreshToken = string.Empty;
        if (request is { Email: not null, Password: not null })
        {
            var user = await identityRepository.GetAsync(request.Email!);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user?.Email!),
                new(ClaimTypes.Role, "User"),
            };

            accessToken = token.GenerateAccessTokenAsync(claims);
            refreshToken = token.GenerateRefreshTokenAsync();
        }

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = DateTime.Now.AddMinutes(15),
        };
    }

    public async Task<TokenResponse> RefreshAsync(RefreshTokenRequest request)
    {
        string accessToken = string.Empty, refreshToken = string.Empty;
        var user = await identityRepository.GetAsync(request.Email!);
        if (request is { RefreshToken: not null })
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user?.Email!),
                new(ClaimTypes.Role, "User"),
            };

            accessToken = token.GenerateAccessTokenAsync(claims);
            refreshToken = token.GenerateRefreshTokenAsync();
        }

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = DateTime.Now.AddMinutes(15),
        };
    }
}

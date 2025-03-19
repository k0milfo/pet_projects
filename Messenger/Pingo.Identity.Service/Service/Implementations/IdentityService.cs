using System.Security.Claims;
using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity;
using Pingo.Identity.Service.Service.Interface;

namespace Pingo.Identity.Service.Service.Implementations;

internal sealed class IdentityService(IIdentityRepository identityRepository, IPasswordHasher hasher, ITokenService token) : IIdentityService
{
    public async Task InsertAsync(RegisterRequest request)
    {
        var salt = hasher.GenerateSalt();
        if (request is { Password: not null, Email: not null })
        {
            var passwordHash = hasher.PasswordHash(request.Password, salt);
            var entity = new User(Guid.NewGuid(), TimeProvider.System.GetUtcNow(), request.Email, passwordHash, salt);
            await identityRepository.InsertAsync(entity);
        }
    }

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
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

            accessToken = token.GenerateAccessToken(claims);
            refreshToken = token.GenerateRefreshToken();
        }

        return Result.Success(new TokenResponse(accessToken, refreshToken, TimeProvider.System.GetUtcNow().AddMinutes(15).UtcDateTime));
    }

    public async Task<Result<TokenResponse>> RefreshAsync(RefreshTokenRequest request)
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

            accessToken = token.GenerateAccessToken(claims);
            refreshToken = token.GenerateRefreshToken();
        }

        return Result.Success(new TokenResponse(accessToken, refreshToken, TimeProvider.System.GetUtcNow().AddMinutes(15).UtcDateTime));
    }
}

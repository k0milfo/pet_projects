using System.Security.Claims;
using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class IdentityService(IIdentityRepository repository, IPasswordHasher hasher, ITokenService token, TimeProvider timeProvider) : IIdentityService
{
    public async Task<UnitResult<LoginErrorType>> InsertAsync(RegisterRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);
        if (request is { Password: not null, Email: not null } && user.Value is null)
        {
            var salt = hasher.GenerateSalt();
            var passwordHash = hasher.PasswordHash(request.Password, salt);
            var entity = new User(Guid.NewGuid(), timeProvider.GetUtcNow(), request.Email, passwordHash, salt);
            await repository.InsertUserAsync(entity);
            return Result.Success<LoginErrorType>();
        }

        return user.Value == null
            ? LoginErrorType.InvalidInputData
            : LoginErrorType.EmailAlreadyExists;
    }

    public async Task<Result<TokenResponse, LoginErrorType>> LoginAsync(AuthRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);
        if (user.Value is not null && request.Password is not null)
        {
            if (!hasher.VerifyPassword(request.Password, user.Value.PasswordHash))
            {
                return LoginErrorType.InvalidPassword;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Value?.Email!),
                new(ClaimTypes.Role, "User"),
            };

            return await ReplaceTokenAsync(request, claims);
        }

        return LoginErrorType.EmailNotFound;
    }

    public async Task<Result<TokenResponse, LoginErrorType>> RefreshTokenAsync(AuthRequest request)
    {
        var refreshToken = repository.GetRefreshTokenAsync(request.Token);
        if (token.IsTokenValid(refreshToken.Result!))
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, request.Email!),
                new(ClaimTypes.Role, "User"),
            };

            return await ReplaceTokenAsync(request, claims);
        }

        return LoginErrorType.InvalidRefreshToken;
    }

    private async Task<TokenResponse> ReplaceTokenAsync(AuthRequest request, List<Claim> claims)
    {
        await repository.DeleteRefreshTokenAsync(new TokenData(request.Token, ExpirationTime: null));
        var accessToken = token.GenerateAccessToken(claims);
        var newRefresh = Guid.NewGuid();
        await repository.InsertRefreshTokenAsync(new TokenData(newRefresh, timeProvider.GetUtcNow()));
        return new TokenResponse(accessToken.Value, newRefresh);
    }
}

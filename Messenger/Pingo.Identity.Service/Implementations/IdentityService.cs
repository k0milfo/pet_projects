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
        if (user is not null)
        {
            return LoginErrorType.EmailAlreadyExists;
        }

        var salt = hasher.GenerateSalt();
        var passwordHash = hasher.PasswordHash(request.Password, salt);
        var entity = new User(Guid.NewGuid(), timeProvider.GetUtcNow(), request.Email, passwordHash, salt);
        await repository.InsertUserAsync(entity);
        return Result.Success<LoginErrorType>();
    }

    public async Task<Result<TokenResponse, LoginErrorType>> LoginAsync(AuthRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);
        if (user is null)
        {
            return LoginErrorType.EmailNotFound;
        }

        if (!hasher.VerifyPassword(request.Password!, user.PasswordHash!))
        {
            return LoginErrorType.InvalidPassword;
        }

        var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user?.Email!),
                new(ClaimTypes.Role, "User"),
            };

        return await ReplaceTokenAsync(new RefreshTokenRequest(request.Email, RefreshToken: null), claims);
    }

    public async Task<Result<TokenResponse, LoginErrorType>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshToken = repository.GetRefreshTokenAsync(request.RefreshToken);
        if (!token.IsTokenValid(refreshToken.Result!))
        {
            return LoginErrorType.InvalidRefreshToken;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, request.Email!),
            new(ClaimTypes.Role, "User"),
        };

        return await ReplaceTokenAsync(request, claims);
    }

    private async Task<TokenResponse> ReplaceTokenAsync(RefreshTokenRequest request, List<Claim> claims)
    {
        await repository.DeleteRefreshTokenAsync(request.RefreshToken);
        var accessToken = token.GenerateAccessToken(claims);
        var newRefresh = Guid.NewGuid();
        await repository.InsertRefreshTokenAsync(new TokenData(newRefresh, timeProvider.GetUtcNow().AddHours(12)));
        return new TokenResponse(accessToken, newRefresh);
    }
}

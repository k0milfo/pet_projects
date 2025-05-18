using System.Security.Claims;
using CSharpFunctionalExtensions;
using MassTransit;
using Pingo.Identity.Events;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class IdentityService(
    IIdentityRepository repository, IPasswordHasher hasher, ITokenService token, TimeProvider timeProvider, IPublishEndpoint publishEndpoint) : IIdentityService
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
        var user = await repository.GetUserAsync(request.Email);
        if (user is null)
        {
            return LoginErrorType.EmailNotFound;
        }

        if (!hasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return LoginErrorType.InvalidPassword;
        }

        await publishEndpoint.Publish(new UserLoggedIn(request.Email),
            ctx => ctx.MessageId = Guid.NewGuid());
        return await ReplaceTokenAsync(user.Email, user.Id);
    }

    public async Task<Result<TokenResponse, LoginErrorType>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshToken = await repository.GetRefreshTokenAsync(request.RefreshToken);
        if (refreshToken is null || !token.IsTokenValid(refreshToken))
        {
            return LoginErrorType.InvalidRefreshToken;
        }

        await repository.DeleteRefreshTokenAsync(request.RefreshToken);
        return await ReplaceTokenAsync(request.Email, refreshToken.UserId);
    }

    private async Task<TokenResponse> ReplaceTokenAsync(string email, Guid id)
    {
        var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, "User"),
            };

        var accessToken = token.GenerateAccessToken(claims);
        var newRefresh = Guid.NewGuid();
        await repository.InsertRefreshTokenAsync(new TokenData(newRefresh, UserId: id, timeProvider.GetUtcNow().AddHours(12)));
        return new TokenResponse(accessToken, newRefresh);
    }

    public async Task ClearingInvalidTokensAsync()
    {
        await repository.DeleteExpiredRefreshTokensAsync(timeProvider.GetUtcNow());
    }
}

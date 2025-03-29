using System.Security.Claims;
using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class IdentityService(IIdentityRepository repository, IPasswordHasher hasher, ITokenService token, TimeProvider timeProvider) : IIdentityService
{
    public async Task<Result> InsertAsync(RegisterRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);
        if (request is { Password: not null, Email: not null } && user.Value is null)
        {
            var salt = hasher.GenerateSalt();
            var passwordHash = hasher.PasswordHash(request.Password, salt);
            var entity = new User(Guid.NewGuid(), timeProvider.GetUtcNow(), request.Email, passwordHash, salt, Token: null);
            await repository.InsertUserAsync(entity);
            return Result.Success();
        }

        return user.Value == null
            ? Result.Failure("Данные введены некорректно")
            : Result.Failure("Пользователь с таким email уже существует.");
    }

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);
        if (user.Value is not null && request.Password is not null)
        {
            if (!hasher.VerifyPassword(request.Password, user.Value.PasswordHash))
            {
                return Result.Failure<TokenResponse>("Неверный пароль.");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Value?.Email!),
                new(ClaimTypes.Role, "User"),
            };

            await token.DeleteRefreshTokenAsync(new RefreshTokenRequest(user.Value!.Email, user.Value.Token));
            var accessToken = token.GenerateAccessToken(claims);
            var refreshToken = token.GenerateRefreshToken(claims);
            await token.InsertRefreshTokenAsync(new RefreshTokenRequest(user.Value!.Email, refreshToken.Value));
            return Result.Success(new TokenResponse(accessToken.Value, refreshToken.Value));
        }

        return Result.Failure<TokenResponse>("Пользователь с таким email не найден.");
    }

    public async Task<Result<TokenResponse>> RefreshAsync(RefreshTokenRequest request)
    {
        var user = await repository.GetUserAsync(request.Email!);

        if (user.Value is not null && user.Value.Token == request.Token && token.IsTokenValid(request.Token!).Value)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Value?.Email!),
                new(ClaimTypes.Role, "User"),
            };

            await token.DeleteRefreshTokenAsync(request);
            var accessToken = token.GenerateAccessToken(claims);
            var refreshToken = token.GenerateRefreshToken(claims);
            await token.InsertRefreshTokenAsync(request);
            return Result.Success(new TokenResponse(accessToken.Value, refreshToken.Value));
        }

        return Result.Failure<TokenResponse>("Refresh-токен недействителен или истёк.");
    }
}

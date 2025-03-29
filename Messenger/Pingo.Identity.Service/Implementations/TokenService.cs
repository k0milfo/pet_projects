using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class TokenService(IOptions<JwtOptions> jwtOptions, TimeProvider timeProvider, IIdentityRepository repository, JwtSecurityTokenHandler tokenHandler) : ITokenService
{
    public Result<string> GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: timeProvider.GetUtcNow().AddMinutes(Convert.ToDouble(jwtOptions.Value.ExpiryInMinutes, CultureInfo.InvariantCulture)).UtcDateTime,
            signingCredentials: creeds);
        return Result.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Result<string> GenerateRefreshToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: timeProvider.GetUtcNow().AddMinutes(Convert.ToDouble(jwtOptions.Value.ExpiryInDays, CultureInfo.InvariantCulture)).UtcDateTime,
            signingCredentials: creeds);
        return Result.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task<Result> DeleteRefreshTokenAsync(RefreshTokenRequest request)
    {
        await repository.DeleteRefreshTokenAsync(request);
        return Result.Success();
    }

    public async Task<Result> InsertRefreshTokenAsync(RefreshTokenRequest request)
    {
        await repository.InsertRefreshTokenAsync(request);
        return Result.Success();
    }

    public Result<bool> IsTokenValid(string token)
    {
        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Value.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return Result.Success(value: true);
        }
        catch (SecurityTokenMalformedException)
        {
            return Result.Success(value: false);
        }
        catch (SecurityTokenExpiredException)
        {
            return Result.Success(value: false);
        }
        catch (SecurityTokenException)
        {
            return Result.Success(value: false);
        }
    }
}

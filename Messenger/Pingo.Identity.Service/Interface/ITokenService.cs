using System.Security.Claims;
using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Requests;

namespace Pingo.Identity.Service.Interface;

internal interface ITokenService
{
    Result<string> GenerateAccessToken(IEnumerable<Claim> claims);

    Result<string> GenerateRefreshToken(IEnumerable<Claim> claims);

    Task<Result> DeleteRefreshTokenAsync(RefreshTokenRequest request);

    Task<Result> InsertRefreshTokenAsync(RefreshTokenRequest request);

    Result<bool> IsTokenValid(string token);
}

using System.Security.Claims;

namespace Pingo.Identity.WebApi.Service.Interface;

public interface ITokenService
{
    string GenerateAccessTokenAsync(IEnumerable<Claim> claims);

    string GenerateRefreshTokenAsync();
}

using System.Security.Claims;

namespace Pingo.Identity.Service.Service.Interface;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();
}

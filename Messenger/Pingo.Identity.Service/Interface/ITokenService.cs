using System.Security.Claims;
using Pingo.Identity.Service.Entity.Models;

namespace Pingo.Identity.Service.Interface;

internal interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    bool IsTokenValid(TokenData token);
}

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class TokenService(IOptions<JwtOptions> jwtOptions, TimeProvider timeProvider) : ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: timeProvider.GetUtcNow().AddMinutes(Convert.ToDouble(jwtOptions.Value.ExpiryInMinutes, CultureInfo.InvariantCulture)).UtcDateTime,
            signingCredentials: creeds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsTokenValid(TokenData token)
    {
        return token.ExpirationTime > timeProvider.GetUtcNow();
    }
}

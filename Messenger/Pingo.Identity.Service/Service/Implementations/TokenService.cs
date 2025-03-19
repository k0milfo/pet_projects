using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pingo.Identity.Service.Entity;
using Pingo.Identity.Service.Service.Interface;

namespace Pingo.Identity.Service.Service.Implementations;

public sealed class TokenService(IOptions<JwtOptions> jwtOptions) : ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: TimeProvider.System.GetUtcNow().AddMinutes(Convert.ToDouble("15", CultureInfo.InvariantCulture)).UtcDateTime,
            signingCredentials: creeds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pingo.Identity.WebApi.Service.Interface;

namespace Pingo.Identity.WebApi.Service.Implementations;

public sealed class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpiryInMinutes"], CultureInfo.InvariantCulture)),
            signingCredentials: creeds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshTokenAsync()
    {
        return Guid.NewGuid().ToString();
    }
}

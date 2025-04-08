using System.Security.Claims;
using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Requests;

namespace Pingo.Identity.Service.Interface;

internal interface ITokenService
{
    Result<string> GenerateAccessToken(IEnumerable<Claim> claims);

    bool IsTokenValid(TokenData token);
}

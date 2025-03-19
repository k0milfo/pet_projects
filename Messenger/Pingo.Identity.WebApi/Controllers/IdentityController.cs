using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.Service.Entity;
using Pingo.Identity.Service.Service.Interface;

namespace Pingo.Identity.WebApi.Controllers;

[ApiController]
[Route("api/identity")]
public sealed class IdentityController(IIdentityService identityService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("register")]
    public async Task<IActionResult> InsertIdentity(RegisterRequest request)
    {
        await identityService.InsertAsync(request);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [HttpPost("login")]
    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var result = await identityService.LoginAsync(request);
        return result.Value;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [HttpPost("refresh")]
    public async Task<TokenResponse> RefreshAsync(RefreshTokenRequest request)
    {
        var result = await identityService.RefreshAsync(request);
        return result.Value;
    }
}

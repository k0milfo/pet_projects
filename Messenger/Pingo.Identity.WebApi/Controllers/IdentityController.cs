using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.WebApi.Entity;
using Pingo.Identity.WebApi.Service.Interface;

namespace Pingo.Identity.WebApi.Controllers;

[ApiController]
[Route("api/identity")]
public sealed class IdentityController(IIdentityService<LoginRequest> identityService) : ControllerBase
{
    [HttpPost("insert")]
    public async Task<IActionResult> InsertIdentity(LoginRequest request)
    {
        await identityService.InsertAsync(request);

        return this.NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await identityService.LoginAsync(request);
        return result is { AccessToken: not "", RefreshToken: not "" } ? Ok(result) : Unauthorized();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await identityService.RefreshAsync(request);
        return result is { AccessToken: not "", RefreshToken: not "" } ? Ok(result) : Unauthorized();
    }
}

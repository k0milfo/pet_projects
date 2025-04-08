using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.Service;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.WebApi.Controllers;

[ApiController]
[Route("api/identity")]
public sealed class IdentityController(IIdentityService identityService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("register")]
    public async Task<IActionResult> InsertIdentity(RegisterRequest request)
    {
        var result = await identityService.InsertAsync(request);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                LoginErrorType.EmailAlreadyExists => Problem(
                    detail: result.Error.ToString(),
                    statusCode: StatusCodes.Status401Unauthorized),
                _ => Problem(detail: result.Error.ToString()),
            };
        }

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthRequest request)
    {
        var result = await identityService.LoginAsync(request);
        if (result.IsFailure)
        {
            return result.Error switch
            {
                LoginErrorType.EmailNotFound => Problem(
                    detail: result.Error.ToString(),
                    statusCode: StatusCodes.Status401Unauthorized),
                LoginErrorType.InvalidPassword => Problem(
                    detail: result.Error.ToString(),
                    statusCode: StatusCodes.Status401Unauthorized),
                _ => Problem(detail: result.Error.ToString()),
            };
        }

        return Ok(result.Value);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync(AuthRequest request)
    {
        var result = await identityService.RefreshTokenAsync(request);
        if (result.IsFailure)
        {
            return result.Error switch
            {
                LoginErrorType.InvalidRefreshToken => Problem(
                    detail: result.Error.ToString(),
                    statusCode: StatusCodes.Status401Unauthorized),
                _ => Problem(detail: result.Error.ToString()),
            };
        }

        return Ok(result.Value);
    }
}

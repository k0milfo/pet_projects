using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.WebApi.Controllers;

[ApiController]
[Route("api/identity")]
public sealed class IdentityController(IIdentityService identityService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("register")]
    public async Task<IActionResult> InsertIdentity(RegisterRequest request)
    {
        var result = await identityService.InsertAsync(request);

        return result.IsSuccess
            ? NoContent()
            : Problem(
            detail: result.Error,
            statusCode: StatusCodes.Status400BadRequest);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var result = await identityService.LoginAsync(request);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error switch
        {
            "Пользователь с таким email не найден." => Problem(
                detail: result.Error,
                statusCode: StatusCodes.Status404NotFound),
            "Неверный пароль." => Problem(
                detail: result.Error,
                statusCode: StatusCodes.Status401Unauthorized),
            _ => Problem(detail: result.Error),
        };
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request)
    {
        var result = await identityService.RefreshAsync(request);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error switch
        {
            "Refresh-токен недействителен или истёк." => Problem(
                detail: result.Error,
                statusCode: StatusCodes.Status401Unauthorized),
            _ => Problem(detail: result.Error),
        };
    }
}

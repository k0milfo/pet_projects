namespace Pingo.Identity.WebApi.Entity;

public sealed class RefreshTokenRequest
{
    public string? RefreshToken { get; set; }

    public string? Email { get; set; }
}

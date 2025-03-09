namespace Pingo.Identity.WebApi.Entity;

public sealed class LoginRequest
{
    public string? Email { get; set; }

    public string? Password { get; set; }
}

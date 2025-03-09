namespace Pingo.Identity.WebApi.Entity;

public sealed class TokenResponse
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime ExpiresIn { get; set; }
}

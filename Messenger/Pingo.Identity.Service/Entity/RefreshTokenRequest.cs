namespace Pingo.Identity.Service.Entity;

public sealed record RefreshTokenRequest(string? RefreshToken, string? Email);

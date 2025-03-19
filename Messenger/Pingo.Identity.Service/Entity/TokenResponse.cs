namespace Pingo.Identity.Service.Entity;

public sealed record TokenResponse(string? AccessToken, string? RefreshToken, DateTime ExpiresIn);

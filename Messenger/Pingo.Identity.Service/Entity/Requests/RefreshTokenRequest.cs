namespace Pingo.Identity.Service.Entity.Requests;

public sealed record RefreshTokenRequest(string? Email, string? Token);

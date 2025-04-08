namespace Pingo.Identity.Service.Entity.Responses;

public sealed record TokenResponse(string? AccessToken, Guid? RefreshToken);

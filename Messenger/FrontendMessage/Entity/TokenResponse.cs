namespace FrontendMessage.Entity;

public sealed record TokenResponse(string AccessToken, Guid RefreshToken);

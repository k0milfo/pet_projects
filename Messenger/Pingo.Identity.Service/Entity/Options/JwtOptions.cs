namespace Pingo.Identity.Service.Entity.Options;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string ExpiryInMinutes { get; set; } = string.Empty;

    public string ExpiryInDays { get; set; } = string.Empty;
}

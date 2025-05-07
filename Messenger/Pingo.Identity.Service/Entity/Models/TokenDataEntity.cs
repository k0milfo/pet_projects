namespace Pingo.Identity.Service.Entity.Models;

internal sealed class TokenDataEntity
{
    public Guid Token { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset ExpirationTime { get; set; }

    public string? Email { get; set; }
}

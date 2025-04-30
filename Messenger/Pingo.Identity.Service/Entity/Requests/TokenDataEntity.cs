namespace Pingo.Identity.Service.Entity.Requests;

internal sealed class TokenDataEntity
{
    public TokenDataEntity()
    {
    }

    public Guid? Token { get; set; }

    public DateTimeOffset? ExpirationTime { get; set; }
}

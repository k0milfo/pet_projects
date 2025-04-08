namespace Pingo.Identity.Service.Entity.Requests;

public sealed record TokenData(Guid? Token, DateTimeOffset? ExpirationTime)
{
    public TokenData()
        : this(Guid.Empty, default)
    {
    }
}

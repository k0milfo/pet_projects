namespace Pingo.Identity.Service.Entity.Requests;

public sealed record TokenData(Guid? Token, DateTimeOffset? ExpirationTime);

namespace Pingo.Identity.Service.Entity.Models;

public sealed record TokenData(Guid Token, Guid UserId, DateTimeOffset ExpirationTime);

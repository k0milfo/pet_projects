namespace Pingo.Identity.WebApi.Entity;

public sealed record User(Guid? Id, DateTimeOffset? SentAt, string Email, string Password);

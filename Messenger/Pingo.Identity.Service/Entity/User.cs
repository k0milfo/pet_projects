namespace Pingo.Identity.Service.Entity;

internal sealed record User(Guid Id, DateTimeOffset SentAt, string Email, string HashPassword, string Salt);

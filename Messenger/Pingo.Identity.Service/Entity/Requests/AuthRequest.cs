namespace Pingo.Identity.Service.Entity.Requests;

public sealed record AuthRequest(string? Email, string? Password, Guid? Token);

namespace Pingo.Identity.Service.Entity.Requests;

public sealed record LoginRequest(string? Email, string? Password);

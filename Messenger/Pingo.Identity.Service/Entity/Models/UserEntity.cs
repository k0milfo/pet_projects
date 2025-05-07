namespace Pingo.Identity.Service.Entity.Models;

internal sealed class UserEntity
{
    public Guid Id { get; set; }

    public DateTime RegisteredAt { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;
}

namespace Pingo.Identity.Service.Entity.Models;

internal sealed class UserEntity
{
    public UserEntity()
    {
    }

    public Guid Id { get; set; }

    public DateTime RegisteredAt { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? Salt { get; set; }
}

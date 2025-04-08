namespace Pingo.Identity.Service.Entity.Models;

internal sealed record User(Guid Id, DateTimeOffset RegisteredAt, string Email, string PasswordHash, string Salt)
{
    public User()
        : this(Guid.Empty, default, default!, default!, default!)
    {
    }
}

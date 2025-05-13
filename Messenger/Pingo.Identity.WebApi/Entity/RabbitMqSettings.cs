namespace Pingo.Identity.WebApi.Entity;

public sealed class RabbitMqSettings
{
    internal string Login { get; init; } = string.Empty;

    internal string Password { get; init; } = string.Empty;

    internal string Host { get; init; } = string.Empty;
}

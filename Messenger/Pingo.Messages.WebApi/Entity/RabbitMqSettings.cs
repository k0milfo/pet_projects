namespace Pingo.Messages.WebApi.Entity;

internal sealed class RabbitMqSettings
{
    public string Login { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Host { get; init; } = string.Empty;
}

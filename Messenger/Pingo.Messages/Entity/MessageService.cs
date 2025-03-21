namespace Pingo.Messages.Entity;

public sealed record MessageService(Guid? Id, string? Text, DateTimeOffset? SentAt, DateTimeOffset? UpdatedAt)
{
    public MessageService()
        : this(Id: null, default, SentAt: TimeProvider.System.GetUtcNow(), UpdatedAt: null)
    {
    }
}

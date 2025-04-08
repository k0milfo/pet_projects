namespace Pingo.Messages.Entity;

internal sealed record MessageEntity(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);

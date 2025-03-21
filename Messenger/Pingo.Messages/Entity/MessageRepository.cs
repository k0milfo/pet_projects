namespace Pingo.Messages.Entity;

internal sealed record MessageRepository(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);

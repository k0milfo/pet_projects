namespace Pingo.Messages.Entity;

internal sealed record Message(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);

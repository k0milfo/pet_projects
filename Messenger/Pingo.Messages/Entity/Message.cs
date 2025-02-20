namespace Pingo.Messages.Entity;

public sealed record Message(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);
